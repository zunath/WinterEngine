using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Awesomium.Core;
using System.Threading;
using WinterEngine.UI.AwesomiumSharpXna;

namespace WinterEngine.UI.AwesomiumUILib
{

    /*
    public enum MouseButton
        {
        Left = 0,
        Middle = 1,
        Right = 2,
        }
    */

    public class AwesomiumUI
    {
        public Texture2D webTexture;

        public WebSession webSession;
        public WebView webView;
        BitFactory.Logging.CompositeLogger logger = null;
        bool finishedLoading = false;

        //public EventHandler LoadCompleted;

        public delegate void OnLoadCompletedDelegate();
        private OnLoadCompletedDelegate OnLoadCompletedHandler;

        public OnLoadCompletedDelegate OnLoadCompleted
        {
            get { return OnLoadCompletedHandler; }
            set { OnLoadCompletedHandler = value; }
        }


        public delegate void OnDocumentCompletedDelegate();
        private OnDocumentCompletedDelegate OnDocumentCompletedHandler;

        public OnDocumentCompletedDelegate OnDocumentCompleted
        {
            get { return OnDocumentCompletedHandler; }
            set { OnDocumentCompletedHandler = value; }
        }

        public bool PauseUIDraw = false;
        public bool PauseUIUpdate = false;


        string basePath;


        public AwesomiumUI()
        {
        }

        public AwesomiumUI(BitFactory.Logging.CompositeLogger _logger)
        {
            logger = _logger;
        }

        public void SetLogger(BitFactory.Logging.CompositeLogger _logger)
        {
            logger = _logger;
        }

        public void Initialize(GraphicsDevice device, int renderTargetWidth, int renderTargetHeight, string _basePath, string customCSS = "")
        {
            basePath = _basePath;

            //OnLoadCompleted = new OnLoadCompletedDelegate();

            //WebCore.Initialize(new WebCoreConfig() { CustomCSS = "::-webkit-scrollbar { visibility: hidden; }" });			
            //WebCore.Initialize(new WebConfig() { CustomCSS = customCSS, SaveCacheAndCookies = true });  //1.7
            //WebCore.Initialize(new WebCoreConfig() { CustomCSS = customCSS, SaveCacheAndCookies = true });
            WebCore.Initialize(new WebConfig() { LogPath = Environment.CurrentDirectory, LogLevel = LogLevel.Verbose });

            webSession = WebCore.CreateWebSession(new WebPreferences() { CustomCSS = "::-webkit-scrollbar { visibility: hidden; }" });

            if (logger != null)
                logger.LogInfo("WEBCORE initialized.");

            webTexture = new Texture2D(device, renderTargetWidth, renderTargetHeight);

            if (logger != null)
                logger.LogInfo("Rendertarget created.");


            webView = WebCore.CreateWebView(renderTargetWidth, renderTargetHeight, webSession);

            //LoadingFrameComplete still seems to take an
            //inordinate amout of time with local files...
            //SOMETIMES.  In this example the first time UI.html is
            //loaded it is near instant.  Go to one of the other 
            //pages with F1-F4, then F5 to return and it takes
            //~20 seconds to be called, well after the page
            //is up and being drawn.
            //As long as you haven't navigated to an online
            //page and back it is instant.  Odd.
            webView.DocumentReady += OnDocumentReadyInternal;
            webView.LoadingFrameComplete += OnLoadingFrameCompleteInternal;


            if (logger != null)
                logger.LogInfo("WebView created.");

            webView.IsTransparent = true;
        }

        public void Shutdown()
        {
            if (logger != null)
                logger.LogInfo("Shutting down WebCore.");

            WebCore.Shutdown();

            webSession.Dispose();
        }


        public void Focus()
        {
            webView.FocusView();
        }


        //Load a URL into the webView.
        public bool Load(string URL)
        {
            bool success = false;

            try
            {
                finishedLoading = false;

                if (logger != null)
                    logger.LogInfo(string.Format("Loading: {0}", URL));

                //There is probably a better way to do this.
                if (URL.ToUpper().Contains("HTTP"))
                    webView.Source = new Uri(URL);
                else
                    webView.Source = new Uri(basePath + "\\" + URL);

                success = true;
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }

            return success;
        }


        private void OnDocumentReadyInternal(object sender, UrlEventArgs e)
        {
            if (logger != null)
                logger.LogInfo("Documente ready.");

            //Callback to let our caller know the page is loaded.
            if (OnDocumentCompletedHandler != null)
                OnDocumentCompletedHandler();
        }


        private void OnLoadingFrameCompleteInternal(object sender, FrameEventArgs e)
        {
            if (e.IsMainFrame)
            {
                finishedLoading = true;
                Focus();

                if (logger != null)
                    logger.LogInfo("Load finished.");

                //Callback to let our caller know the page is loaded.
                if (OnLoadCompletedHandler != null)
                    OnLoadCompletedHandler();
            }
        }

        public void RenderWebView()
        {
            if (!PauseUIDraw)
            {
                BitmapSurface surface = (BitmapSurface)webView.Surface;



                if (surface != null)
                    surface.RenderTexture2D(webTexture);
            }
        }

        public void Update()
        {
            if (!PauseUIUpdate)
            {
                WebCore.Update();


                if (webView.Surface != null)
                    if (((BitmapSurface)webView.Surface).IsDirty && webTexture != null)
                        RenderWebView();
            }
        }


        //http://wiki.awesomium.com/javascript-integration/introduction-to-javascript-integration.html

        public JSObject CreateJSObject(string objectName)
        {
            return CreateGlobalJSObject(objectName);
        }

        //Global JS Objects persist between pages.
        public JSObject CreateGlobalJSObject(string objectName)
        {
            //webView.CreateObject(objectName); 
            return webView.CreateGlobalJavascriptObject(objectName);
        }

        public void CreateJSObject(string objectName, string method, JavascriptMethodEventHandler callback)
        {
            JSObject jsObject = CreateJSObject(objectName);

            //This used to be SetObjectCallback.
            jsObject.Bind(method, false, callback);
        }

        public object CallJavascriptWithResult(string javaScript)
        {
            JSValue val = new JSValue();

            if (webView.IsDocumentReady)
                val = webView.ExecuteJavascriptWithResult(javaScript);

            return val;
        }

        public void CallJavascript(string javaScript)
        {
            if (webView.IsDocumentReady)
                webView.ExecuteJavascript(javaScript);
        }

        //Need to make a general method to pass arbitrary
        //number of args through.
        public void CallJavascriptFunction(string function, string message, string objectName = "")
        {
            //JSValue[] args = new JSValue[1];
            //args[0] = new JSValue(message);



            if (webView.IsDocumentReady)
                webView.ExecuteJavascript(function + "('" + message + "');");
        }


        #region input

        public void InjectKeyboardEvent(int msg, int wParam, int lParam)
        {
            //webView.InjectKeyboardEventWin((int)msg, (int)wParam, (int)lParam);

            Modifiers modifiers = new Modifiers();


            WebKeyboardEvent keyEvent = new WebKeyboardEvent((uint)msg, (IntPtr)wParam, (IntPtr)lParam, modifiers);

            webView.InjectKeyboardEvent(keyEvent);
        }



        public void InjectMouseMove(int x, int y)
        {
            webView.InjectMouseMove(x, y);
        }

        public void InjectMouseDown(WinMouseButton mouseButton)
        {
            webView.InjectMouseDown((Awesomium.Core.MouseButton)((int)mouseButton - 1));
        }

        public void InjectMouseUp(WinMouseButton mouseButton)
        {
            webView.InjectMouseUp((Awesomium.Core.MouseButton)((int)mouseButton - 1));
        }

        public void MouseUpHandler(WinMouseButton mouseButton)
        {
            webView.InjectMouseUp((Awesomium.Core.MouseButton)((int)mouseButton - 1));
        }
        #endregion


    }
}
