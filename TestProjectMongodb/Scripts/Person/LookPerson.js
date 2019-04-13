//$(function () {
    
//    function DelPersonAjax(ObjectId) {
//        $.getJSON(
//            "../Person/DeletePerson",
//            { _id: ObjectId },
//            function (Result) {
//                if (Result.Result == "true")
//                    window.location.href = window.location.href;
//                else
//                    alert("操作有误");

//            })
//    }
//})

function DelPersonAjax(ObjectId) {
    $.getJSON(
        "../Person/DeletePerson",
        { _id: ObjectId },
        function (Result) {
            if (Result.Result == true)
                window.location.reload();
            else
                alert("操作有误");

        })
}