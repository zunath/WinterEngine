using System;
using System.Runtime.InteropServices;
using Awesomium.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AwesomiumXNA
{
    public class AwesomiumComponent : DrawableGameComponent
    {
        private delegate Int32 ProcessMessagesDelegate(Int32 code, Int32 wParam, ref Message lParam);

        private static class User32
        {
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr SetWindowsHookEx(Int32 windowsHookId, ProcessMessagesDelegate function, IntPtr mod, Int32 threadId);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern Int32 UnhookWindowsHookEx(IntPtr hook);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern Int32 CallNextHookEx(IntPtr hook, Int32 code, Int32 wParam, ref Message lParam);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern Boolean TranslateMessage(ref Message message);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr FindWindow(String className, String windowName);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern int RegisterWindowMessage(String msg);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(HandleRef hWnd, Int32 msg, Int32 wParam, Int32 lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool SystemParametersInfo(Int32 nAction, Int32 nParam, ref Int32 value, Int32 ignore);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern int GetSystemMetrics(Int32 nIndex);
        }

        private static class Kernel32
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern Int32 GetCurrentThreadId();
        }

        private static class SystemMetrics
        {
            internal static Int32 MouseWheelScrollDelta
            {
                get
                {
                    return 120;
                }
            }

            internal static Int32 MouseWheelScrollLines
            {
                get
                {
                    var scrollLines = 0;

                    if (User32.GetSystemMetrics(75) == 0)
                    {
                        var hwnd = User32.FindWindow("MouseZ", "Magellan MSWHEEL");
                        if (hwnd != IntPtr.Zero)
                        {
                            var windowMessage = User32.RegisterWindowMessage("MSH_SCROLL_LINES_MSG");
                            scrollLines = (Int32)User32.SendMessage(new HandleRef(null, hwnd), windowMessage, 0, 0);
                            if (scrollLines != 0)
                            {
                                return scrollLines;
                            }
                        }
                        return 3;
                    }
                    User32.SystemParametersInfo(104, 0, ref scrollLines, 0);
                    return scrollLines;
                }
            }
        }

        private enum WindowsMessage
        {
            KeyDown = 0x0100,
            KeyUp = 0x0101,
            Char = 0x0102,

            MouseMove = 0x0200,
            LeftButtonDown = 0x0201,
            LeftButtonUp = 0x0202,
            LeftButtonDoubleClick = 0x0203,

            RightButtonDown = 0x0204,
            RightButtonUp = 0x0205,
            RightButtonDoubleClick = 0x0206,

            MiddleButtonDown = 0x0207,
            MiddleButtonUp = 0x0208,
            MiddleButtonDoubleClick = 0x0209,

            MouseWheel = 0x020A,
        }

        private struct Message
        {
            internal IntPtr HWnd;
            internal Int32 Msg;
            internal IntPtr WParam;
            internal IntPtr LParam;
            internal IntPtr Result;
        }


        private IntPtr hookHandle;
        private ProcessMessagesDelegate processMessages;


        private IntPtr imagePtr;
        private Byte[] imageBytes;

        private Rectangle area;
        private Rectangle? newArea;
        private Boolean resizing;

        public AwesomiumComponent(Game game, Rectangle area)
            : base(game)
        {
            Init(game, area);
        }

        public AwesomiumComponent(Game game)
            : base(game)
        {
            Init(game, Rectangle.Empty);
        }

        private void Init(Game game, Rectangle area)
        {
            this.area = area;

            // 2013-08-16 (zunath): Added check to make sure we don't reinitialize the WebCore
            if (!WebCore.IsRunning)
            {
                WebCore.Initialize(new WebConfig());
            }
            WebView = WebCore.CreateWebView(area.Width, area.Height);

            while (WebView.IsLoading)
            {
                WebCore.Update();
            }

            //BitmapSurface asdf = ((BitmapSurface)WebView.Surface);
            //WebView.Resized += WebView_ResizeComplete;
            
            //WebView.FlushAlpha = false;
            WebView.IsTransparent = true;

            // WebView doesn't seem to listen when I say this
            //WebView.SelfUpdate = true;
            // So I have to say this:
            WebCore.AutoUpdatePeriod = 10000000;   // TEEENN MIILLLIOON
            
            processMessages = ProcessMessages;

            // Create the message hook.
            hookHandle = User32.SetWindowsHookEx(3, processMessages, IntPtr.Zero, Kernel32.GetCurrentThreadId());

            WebView.FocusView();
        }


        ~AwesomiumComponent()
        {
            // Remove the message hook.
            if (hookHandle != IntPtr.Zero)
                User32.UnhookWindowsHookEx(hookHandle);
        }

        public Texture2D WebViewTexture { get; private set; }

        public WebView WebView { get; private set; }

        public Rectangle Area
        {
            get { return area; }
            set
            {
                newArea = value;
            }
        }

        private Int32 ProcessMessages(Int32 code, Int32 wParam, ref Message lParam)
        {
            if (code == 0 && wParam == 1)
            {
                switch ((WindowsMessage)lParam.Msg)
                {
                    case WindowsMessage.KeyDown:
                    case WindowsMessage.KeyUp:
                    case WindowsMessage.Char:
                        WebView.InjectKeyboardEvent(new WebKeyboardEvent((uint)lParam.Msg, lParam.WParam, lParam.LParam, 0));
                        break;

                    case WindowsMessage.MouseMove:
                        var mouse = Mouse.GetState();
                        WebView.InjectMouseMove(mouse.X - area.X, mouse.Y - area.Y);
                        break;

                    case WindowsMessage.LeftButtonDown:
                        WebView.InjectMouseDown(MouseButton.Left);
                        break;
                    case WindowsMessage.LeftButtonUp:
                        WebView.InjectMouseUp(MouseButton.Left);
                        break;
                    case WindowsMessage.LeftButtonDoubleClick:
                        WebView.InjectMouseDown(MouseButton.Left);
                        break;

                    case WindowsMessage.RightButtonDown: WebView.InjectMouseDown(MouseButton.Right); break;
                    case WindowsMessage.RightButtonUp: WebView.InjectMouseUp(MouseButton.Right); break;
                    case WindowsMessage.RightButtonDoubleClick: WebView.InjectMouseDown(MouseButton.Right); break;

                    case WindowsMessage.MiddleButtonDown: WebView.InjectMouseDown(MouseButton.Middle); break;
                    case WindowsMessage.MiddleButtonUp: WebView.InjectMouseUp(MouseButton.Middle); break;
                    case WindowsMessage.MiddleButtonDoubleClick: WebView.InjectMouseDown(MouseButton.Middle); break;

                    case WindowsMessage.MouseWheel:
                        var delta = (((Int32)lParam.WParam) >> 16);
                        WebView.InjectMouseWheel(delta / SystemMetrics.MouseWheelScrollDelta * 16 * SystemMetrics.MouseWheelScrollLines, 0);
                        break;
                }
                User32.TranslateMessage(ref lParam);
            }

            return User32.CallNextHookEx(IntPtr.Zero, code, wParam, ref lParam);
        }

        public void Resize(int width, int height)
        {
            area.Width = width;
            area.Height = height;
            WebViewTexture = new Texture2D(Game.GraphicsDevice, area.Width, area.Height, false, SurfaceFormat.Color);
            WebView.Resize(area.Width, area.Height);

        }

        protected override void LoadContent()
        {
            if (area.IsEmpty)
            {
                area = GraphicsDevice.Viewport.Bounds;
                newArea = GraphicsDevice.Viewport.Bounds;
            }
            WebViewTexture = new Texture2D(Game.GraphicsDevice, area.Width, area.Height, false, SurfaceFormat.Color);
            imageBytes = new Byte[area.Width * 4 * area.Height];
            imagePtr = Marshal.AllocHGlobal(imageBytes.Length);
        }

        public override void Update(GameTime gameTime)
        {
            //if (newArea.HasValue && !resizing && gameTime.TotalGameTime.TotalSeconds > 0.10f)
            //{
            //    area = newArea.Value;
            //    if (area.IsEmpty)
            //        area = GraphicsDevice.Viewport.Bounds;

            //    ((BitmapSurface)WebView.Surface).Resized += WebView_ResizeComplete;
            //    WebView.Resize(area.Width, area.Height);
            //    WebViewTexture = new Texture2D(Game.GraphicsDevice, area.Width, area.Height, false, SurfaceFormat.Color);
            //    imageBytes = new Byte[area.Width * 4 * area.Height];
            //    imagePtr = Marshal.AllocHGlobal(imageBytes.Length);
            //    resizing = true;

            //    newArea = null;
            //}

            // Manually update the webcore so that we're not running 2 clocks
            WebCore.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (WebView.Surface != null && ((BitmapSurface)WebView.Surface).IsDirty)
            {
                BitmapSurface renderBuffer = ((BitmapSurface)WebView.Surface);
#if false
				// This was the original solution
				renderBuffer.CopyTo(imagePtr, renderBuffer.Width * 4, 4, true, false);
				Marshal.Copy(imagePtr, imageBytes, 0, imageBytes.Length);
				WebViewTexture.SetData(imageBytes);
#endif
#if false
				// This was MindworX's attempt to make it faster, and it's just barely faster than the above
				unsafe
				{
					// This part saves us from double copying everything.
					fixed (Byte* imagePtr = imageBytes)
					{
						renderBuffer.CopyTo((IntPtr)imagePtr, renderBuffer.Width * 4, 4, false, false);
					}
				}
				WebViewTexture.SetData(imageBytes);
#endif
#if true
                // Found this little trick online, and it's quite a lot faster than either method above (roughly 3x faster)
                renderBuffer.RenderTexture2D(WebViewTexture);
#endif
            }

            base.Draw(gameTime);
        }
    }
}
