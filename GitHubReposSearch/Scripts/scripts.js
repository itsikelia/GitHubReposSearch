
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
                data: "{'repoName':'" + repName + "','owner':'" + ownerName + "' }", //Pass the parameter names and values
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            });
        
    });


    $("#search_submit").on("click", function () {
        $.ajax({
            type: "GET",
            url: '/GitSearch/SearchInfo?data=' + $("#search").val(),
            success: function (result) {
                window.location.href = '/GitSearch/SearchInfo?data=' + $("#search").val();
            }
        });

    })
   

});










