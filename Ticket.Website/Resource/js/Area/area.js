layui.use(['form', 'layedit', 'laydate'], function () {
    var form = layui.form
  , layer = layui.layer
  , layedit = layui.layedit
  , admin = layui.admin
  , laydate = layui.laydate;
    form.on('select(Select1)', function (data) {
        console.log(data.elem); //得到select原始DOM对象
        console.log(data.value); //得到被选中的值
        console.log(data.othis); //得到美化后的DOM对象
    });
    form.on('select(Select2)', function (data) {
        console.log(data.elem); //得到select原始DOM对象
        console.log(data.value); //得到被选中的值
        console.log(data.othis); //得到美化后的DOM对象
    });
    form.on('select(Select3)', function (data) {
        console.log(data.elem); //得到select原始DOM对象
        console.log(data.value); //得到被选中的值
        console.log(data.othis); //得到美化后的DOM对象
    });

    var $s1 = $("#Select1");
    var $s2 = $("#Select2");
    var $s3 = $("#Select3");
    var v1 = null;
    var v2 = null;
    var v3 = null;
    $s1.html("<option>请选择省份</option>");
    $.each(threeSelectData, function (k, v) {
        appendOptionTo($s1, k, v.val, v1);
    });
    form.on('select(Select1)', function (data) {
        $s2.html("<option value='|'>请选择城市</option>");
        $s3.html("<option valkue='|'>请选择区县</option>");
        if (this.selectedIndex == -1) return;
        //var s1_curr_val = this.options[this.selectedIndex].value;
        var s1_curr_val = data.value;
        $.each(threeSelectData, function (k, v) {
            if (s1_curr_val == v.val) {
                if (v.items) {
                    $.each(v.items, function (k, v) {
                        appendOptionTo($s2, k, v.val, v2);
                    });
                }
            }
        });
        if ($s2[0].options.length == 0) { appendOptionTo($s2, "...", "", v2); }
        $s2.change();
    });
    form.on('select(Select2)', function (data) {
        $s3.html("<option value='|'>请选择区县</option>");
        //var s1_curr_val = $s1[0].options[$s1[0].selectedIndex].value;
        var s1_curr_val = $s1.val();
        if (this.selectedIndex == -1) return;
        //var s2_curr_val = this.options[this.selectedIndex].value;
        var s2_curr_val = data.value;
        var ticketPrefix = "";
        if (s2_curr_val.length == 3) {
            ticketPrefix = s2_curr_val;
        }
        else if (s2_curr_val.length == 4) 
        {
            ticketPrefix=s2_curr_val.substring(1, 4)
        }
        $("#TicketPrefix").val(ticketPrefix);
        $.each(threeSelectData, function (k, v) {
            if (s1_curr_val == v.val) {
                if (v.items) {
                    $.each(v.items, function (k, v) {
                        if (s2_curr_val == v.val) {
                            $.each(v.items, function (k, v) {
                                appendOptionTo($s3, k, v, v3);
                            });
                        }
                    });
                    if ($s3[0].options.length == 0) { appendOptionTo($s3, "...", "", v3); }
                }
            }
        });
    });
    function appendOptionTo($o, k, v, d) {
        var $opt = $("<option>").text(k).val(v);
        if (v == d) { $opt.attr("selected", "selected") }
        $opt.appendTo($o);
        form.render('select');
        $o.get(0).selectedIndex = 0;
    }
}); 