
function ReplaceTargetWithAspActionResult(event, htmlElement, targetId, onSucessFunc = null, onSuccessArgs = null) { 
    event?.preventDefault();
    let actionLink = typeof htmlElement === 'string'? htmlElement : (htmlElement.attributes.href ?? htmlElement.attributes.formaction).value;  //Attribute HREF is for ANCHORS, FORMACTION is for BUTTONS (Sidenote: wonder what attribute for input?)
    
    $.get(
        actionLink, 
        function (result) {    
            $('#' + targetId).html(result);  
            CallFunc(onSucessFunc, onSuccessArgs);
        }
    );
}
function CallFunc(onSucessFunc, onSuccessArgs) {
    if (onSucessFunc !== null) {
        if (Array.isArray(onSuccessArgs))
            onSucessFunc.apply(this, onSuccessArgs);
        else onSucessFunc(onSuccessArgs);
    }
}

// Nice method I found that formats the formdata into an object that can be interpreted as a view model   Ex result,  let formData = {Name: "Charles", Age: 5};
function GetFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};
    $.map(unindexed_array, function (n, i) {
        if (indexed_array[n['name']] === undefined) // A little fix for checkboxes, (Asp-for? adds an extra hidden checkbox at the end of form for some reason with always false value)
            indexed_array[n['name']] = n['value'];
        else if (indexed_array[n['name']] !== 'true') //This if statement will be run if the "form-input-element-id" already have gotten a value (Aka for multiple selects). Also the asp-for hidden checkbox we dont want to make an array, so check if its "true" then dont make array
        {
            //console.log(indexed_array[n['name']]);
            //A little fix for multiple select, since if many selects it will pass (name: 1 value: 1), (name: 1 value: 2) and the second will overwrite the first one, So now if we discover multiple with same name, we make an array
            if (Array.isArray(indexed_array[n['name']]))
                indexed_array[n['name']] = [...indexed_array[n['name']], n['value']];
            else
                indexed_array[n['name']] = [indexed_array[n['name']], n['value']];
            
        }
    });

    return indexed_array;
}


function SubmitForm(event, htmlForm, targetId, resetForm = false, onSucessFunc = null, onSuccessArgs = null) {
    event.preventDefault();
    if (typeof htmlForm === 'string' || htmlForm instanceof String)
        htmlForm = $(htmlForm)[0]; //in case of passing an Id  for a form instead of the actual form

    $.post(htmlForm.action,
        GetFormData($(htmlForm)),
        function (output, status) {
            if (resetForm)
                ResetForm(htmlForm);
            $("#" + targetId).html(output);
            CallFunc(onSucessFunc, onSuccessArgs);
        }).fail(function (output) {
            let htmlResponse = $.parseHTML(output.responseText, document, true);
            let theFormHtml = htmlResponse.find(n => n.id === htmlForm.id);
            if (theFormHtml != null)
                ReplaceSpans(htmlForm, theFormHtml);
        }, "html"); 
}

function ReplaceSpans(htmlForm, newForm) { //Replacing only the spans (Which contain modelstate validation error info) -> dont have to re-send select list data
    let oldSpans = $(htmlForm).find("span");
    let newSpans = $(newForm).find("span");
    for (let i = 0; i < oldSpans.length; i++) {
        oldSpans[i].replaceWith(newSpans[i]);
    }
}

function ResetForm(htmlForm) {

    for (let i = 0; i < htmlForm.length; i++) {
        if (htmlForm[i].type == "text")
            htmlForm[i].value = ""; 
        //else if (htmlForm[i].type == "select-one")
          //  htmlForm[i].selectedIndex = 0;
        else if (htmlForm[i].type == "select-multiple")
            htmlForm[i].selectedIndex = -1;
    }

    // Clear error text from asp-validation spans
    let spans = htmlForm.getElementsByTagName("span");
    for (let i = 0; i < spans.length; i++) {
        spans[i].innerHTML = "";
    }
}

function ToggleHtmlElement(targetId) {
    var x = document.getElementById(targetId);
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}



function CreatePersonSuccess() {
    ToggleHtmlElement('create-person-form-container');
    document.getElementById('btn-add-person').classList.toggle('btn-warning');
    peoplesearchform.PageNumber.value = 0; // Pagenumber managing could be made cleaner !
    let htmlId = document.getElementById('personlist').children[1].id;
    Flash(htmlId);
}

let flashHandler;
let flashCounter = -1;
function Flash(htmlId) {
    let htmlElement = document.getElementById(htmlId);
    if (htmlElement !== undefined && htmlElement !== null && flashCounter < 0) {
        clearInterval(flashHandler);
        flashCounter = 15;
        flashHandler = setInterval(FlashLogic, 80, htmlElement);
    }
}
function FlashLogic(htmlElement) {
    htmlElement.style = flashCounter-- % 2 == 0 ? "background-color: #55ff55;" : "background-color: #66ee66;";
    if (flashCounter < 0) {
        htmlElement.style = "background-color: 0;";
    }
}



function PasswordToggle(button, targetId) {
    var x = document.getElementById(targetId);
    if (x.type === "text") {
        x.type = "password";
        button.value = "Show Password";
    } else {
        x.type = "text";
        button.value = "Hide Password";
    }
}