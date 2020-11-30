
/*
 *  Usage
 * <a onclick="ReplaceTargetWithAspActionResult(event, this, 'IdOfHtmlElement');" asp-controller="Register" asp-action="EditPerson" asp-route-id="@item.Id">Btn</a>
 * 
 */
function ReplaceTargetWithAspActionResult(event, htmlElement, targetId) {  //By doing parameters like this, you can call this method by MethodName(this);  from html
    event.preventDefault();
    let id = 1;
    //const htmlElement = event.currentTarget;
    $.get(
        htmlElement.attributes.href.value, // + "/" + id,
        function (result) {                                     //If successful this function will run
            $('#' + targetId).html(result);   // Replace the innerhtml of target with result (which is the partialview returned by the controller)
            
            /* Side note
             * //document.getElementById(targetId).innerHTML = result; //When doing this instead of using Jquery the Create button doesnt work?
             * Why?
             *
                Setting the innerHTML property does not execute scripts.
                jQuery contains special code to execute scripts for you.
             * 
             */
        }
    ).fail(
        function (result) {
            alert(result);
        }
    );
}

function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}

function SubmitEdit(event, htmlForm, entryId, targetId, actionLink) {
    event.preventDefault();

    $.ajax({
        data: {
            id: entryId,
            model: JSON.stringify(getFormData($(htmlForm)))
            //model: JSON.stringify($('#editpersonform').serializeArray())
            //JSON.stringify(person)},//{ model: JSON.stringify($('#editpersonform')) },
        },
        type: "POST",
        url: actionLink, //htmlForm.attributes.href.value, //"/Register/EditPerson2", //Im a bit confuse, like isnt this opening up to overposting ?

        success: function (output) {
            //code
            $("#" + targetId).html(output);
        }

    })
}