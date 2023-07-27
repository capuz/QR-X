
    // 1st replace first column header text with checkbox
    function gridFunctionCheckAll() {

        $("#gridCheckAll th").each(function () {
            if ($.trim($(this).text()) === "{checkall}") {
                $(this).text('');
                $("<input/>", { type: "checkbox", id: "cbSelectAll", value: "", title: "Seleccionar/Deseleccionar Todo" }).appendTo($(this));
            }
        });

        //2nd click event for header checkbox for select /deselect all
        $("#cbSelectAll").on("click", function() {
            var ischecked = this.checked;
            $('#gridCheckAll').find("input:checkbox").each(function () {
                this.checked = ischecked;
            });
        });

        //3rd click event for checkbox of each row
        $("input[name='checkboxId']").click(function () {
            var totalRows = $("#gridCheckAll td :checkbox").length;
            var checked = $("#gridCheckAll td :checkbox:checked").length;

            if (checked == totalRows) {
                $("#gridCheckAll").find("input:checkbox").each(function () {
                    this.checked = true;
                });
            } else {
                $("#cbSelectAll").removeAttr("checked");
            }
        });
    }
