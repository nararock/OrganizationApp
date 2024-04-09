function generateClickInput(id) {
    let input = document.querySelector("#organizationInput");
    let inputId = document.querySelector("#organizationId");
    inputId.value = id;
    let event = new MouseEvent("click");
    input.dispatchEvent(event);
}

function generateClickButton() {
    let input = document.querySelector("#formButton");
    let event = new MouseEvent("click");
    input.dispatchEvent(event);
}

function generateClickUploadOrganization() {
    let input = document.querySelector("#uploadOrganization");
    let event = new MouseEvent("click");
    input.dispatchEvent(event);
}

function generateClickUploadButton() {
    let input = document.querySelector("#formUploadButton");
    let event = new MouseEvent("click");
    input.dispatchEvent(event);
}