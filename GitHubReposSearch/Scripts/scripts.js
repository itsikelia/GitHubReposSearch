
$(document).ready(function () {
    $(".BookMarked").on("click", function () {
        var method;
        if (this.checked) {
            method = "addBookMark";
        }
        else {
            method = "removeBookMark";
        }
            var splittedID = this.parentNode.parentNode.id.split("+");
            var repName = splittedID[0]; var ownerName = splittedID[1];
            $.ajax({
                type: "POST",
                url: "/GitSearch/" + method,
                data: "{'repoName':'" + repName + "','owner':'" + ownerName + "' }", 
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            });
        
    });


   

});










