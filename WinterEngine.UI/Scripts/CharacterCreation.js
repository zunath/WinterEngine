function Initialize() {
    $('.clsFormValidation').validate();
    InitializeCharacterCreationViewModel();
}

function ChangeActiveMode(mode) {
    CharacterCreationViewModel.CurrentMode(mode);
}

function CreateCharacter() {
    var model = ko.viewmodel.toModel(CharacterCreationViewModel);
    Entity.CreateCharacter(JSON.stringify(model));
}

function CreateCharacter_Callback() {
}

function CancelCharacterCreation() {
    Entity.CancelCharacterCreation();
}


function SelectRace(index) {
    CharacterCreationViewModel.ActiveRaceIndex(index);
    Entity.SelectRace(index);
}

function SelectRace_Callback() {
}

function SelectClass(index) {
    CharacterCreationViewModel.ActiveClassIndex(index);
    Entity.SelectClass(index);
}

function SelectClass_Callback() {
}

function SelectAbility(index) {
    CharacterCreationViewModel.ActiveAbilityIndex(index);
    Entity.SelectAbility(index);
}

function SelectAbility_Callback() {
}

function SelectSkill(index) {
    CharacterCreationViewModel.ActiveSkillIndex(index);
    Entity.SelectSkill(index);
}

function SelectSkill_Callback() {
}

function SelectPortrait(index) {
    CharacterCreationViewModel.ActivePortraitIndex(index);
    Entity.SelectPortrait(index);
}

function SelectPortrait_Callback() {
}