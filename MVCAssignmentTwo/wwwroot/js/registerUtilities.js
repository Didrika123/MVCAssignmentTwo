
function ReplaceTargetWithAspActionResult(event, htmlElement, targetId) {  //By doing parameters like this, you can call this method by MethodName(this);  from html
    event.preventDefault();
    let actionLink = (htmlElement.attributes.href ?? htmlElement.attributes.formaction).value;  //Attribute HREF is for ANCHORS, FORMACTION is for BUTTONS (Sidenote: wonder what attribute for input?)
    
    $.get(
        actionLink, 
        function (result) {                   //If successful this function will run
            $('#' + targetId).html(result);   // Replace the innerhtml of target with result (which is the partialview returned by the controller)
            
            /* Side note
             * //document.getElementById(targetId).innerHTML = result; //When doing this instead of using Jquery the Create button doesnt work?
             * Why?
                Setting the innerHTML property does not execute scripts.
                jQuery contains special code to execute scripts for you.
             */
        }
    ).fail(
        function (result) {
        }
    );
}

// Nice method I found that formats the formdata into an object that can be interpreted as a view model   Ex result,  let formData = {Name: "Charles", Age: 5};
function GetFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}


function SubmitForm(event, htmlForm, targetId) {
    event.preventDefault();
    if (typeof htmlForm === 'string' || htmlForm instanceof String)
        htmlForm = $(htmlForm)[0]; //in case of passing an Id  for a form instead of the actual form
    console.log(htmlForm.action);
    $.post(htmlForm.action,
        GetFormData($(htmlForm)),
        function (output, status) {
           // htmlForm.reset(); https://www.geeksforgeeks.org/how-to-reset-a-form-using-jquery-with-reset-method/
            $("#" + targetId).html(output);
        }).fail(function (output) {
            let htmlResponse = $.parseHTML(output.responseText, document, true);
            let theFormHtml = htmlResponse.find(n => n.id === htmlForm.id);
            if(theFormHtml != null)
                htmlForm.replaceWith(theFormHtml);
        });

}

function ToggleHtmlElement(targetId) {
    var x = document.getElementById(targetId);
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}
