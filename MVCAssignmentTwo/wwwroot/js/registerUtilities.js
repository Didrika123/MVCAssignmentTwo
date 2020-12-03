
function ReplaceTargetWithAspActionResult(event, htmlElement, targetId) { 
    event.preventDefault();
    let actionLink = (htmlElement.attributes.href ?? htmlElement.attributes.formaction).value;  //Attribute HREF is for ANCHORS, FORMACTION is for BUTTONS (Sidenote: wonder what attribute for input?)
    
    $.get(
        actionLink, 
        function (result) {    
            $('#' + targetId).html(result);  
        }
    );
}

// Nice method I found that formats the formdata into an object that can be interpreted as a view model   Ex result,  let formData = {Name: "Charles", Age: 5};
function GetFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};
    $.map(unindexed_array, function (n, i) {
        if (indexed_array[n['name']] === undefined) // A little fix for checkboxes, (Asp-for? adds an extra hidden checkbox at the end of form for some reason with always false value)
            indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}


function SubmitForm(event, htmlForm, targetId, resetForm = false) {
    event.preventDefault();
    if (typeof htmlForm === 'string' || htmlForm instanceof String)
        htmlForm = $(htmlForm)[0]; //in case of passing an Id  for a form instead of the actual form

    $.post(htmlForm.action,
        GetFormData($(htmlForm)),
        function (output, status) {
            if (resetForm)
                ResetForm(htmlForm);
            $("#" + targetId).html(output);
        }).fail(function (output) {
            let htmlResponse = $.parseHTML(output.responseText, document, true);
            let theFormHtml = htmlResponse.find(n => n.id === htmlForm.id);
            if(theFormHtml != null)
                htmlForm.replaceWith(theFormHtml);
        });

}

function ResetForm(htmlForm) {
    for (let i = 0; i < htmlForm.length; i++)
        if(htmlForm[i].type == "text")
            htmlForm[i].value = "";
}

function ToggleHtmlElement(targetId) {
    var x = document.getElementById(targetId);
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}
