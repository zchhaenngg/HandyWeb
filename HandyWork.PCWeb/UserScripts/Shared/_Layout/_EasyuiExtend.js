(function ($) {
    $.extend($.fn.validatebox.defaults, {
        codeRequired: false,//非空校验默认
    });

    $.fn.combobox.defaults.cascade = function (routeValue, options) {
        if (routeValue) {
            $("#" + options.id).combobox({
                url: options.url + routeValue,
                valueField: options.valueField,
                textField: options.textField,
                onSelect: function (record) {
                    if (options.onSelect) {
                        options.onSelect(record);
                    }
                    if (options.options) {
                        $.fn.combobox.defaults.cascade(eval("record." + options.valueField), options.options);
                    }
                },
                onLoadSuccess: function (result) {
                    if (result.length > 0) {
                        $(this).combobox('enable');
                        var val = $(this).combobox("getData");
                        for (var item in val[0]) {
                            if (item == options.valueField) {
                                $(this).combobox("select", val[0][item]);
                                break;
                            }
                        }
                    } else {
                        $(this).combobox('disable');
                    }
                },
                onLoadError: function () {
                    $(this).combobox('disable');
                }
            });
        }
    }
})(jQuery);
(function () {
    $.extend($.fn.validatebox.defaults.rules, {
        notEqualChoose: {
            validator: function (value, param) {
                if (value == "--请选择--") {
                    $.fn.validatebox.defaults.rules.notEqualChoose.message = "请选择其他选项";
                    return false;
                }
                if (undefined != param && param[0]) {
                    var data = $('#' + param[0]).combobox('getData');
                    var success = false;
                    $.each(data, function (i, n) {
                        if (value == n.text || value == n.Text) {
                            success = true;
                        }
                    });
                    $.fn.validatebox.defaults.rules.notEqualChoose.message = "请选择正确项";
                    return success;
                }
                return true;
            },
            message: "请选择其他选项"
        },
        isEqual: {
            validator: function (value, param) {
                var success = false;
                if (value != null) {
                    var data = $('#' + param[0]).combobox('getData');
                    $.each(data, function (i, n) {
                        if (value == n.text || value == n.Text) {
                            success = true;
                        }
                    });
                }
                return (success);
            },
            message: $.fn.validatebox.defaults.rules.invalidMessage
        },
        isEqualAndNotEmpty: {
            validator: function (value, param) {
                if ($.trim(value) == "")
                    return false;
                var success = false;
                if (value != null) {
                    var val = $('#' + param[0]).combobox('getValue');
                    if (val) {
                        success = true;
                    }
                }
                $.fn.validatebox.defaults.rules.isEqualAndNotEmpty.message = $.fn.validatebox.defaults.rules.invalidMessage;
                return (success);
            },
            message: $.fn.validatebox.defaults.rules.invalidMessage
        },
        ip: {
            validator: function (value, param) {
                var reg = /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/;
                return (reg.test(value));
            },
            message: $.fn.validatebox.defaults.rules.ipMessage
        },
        dateboxformat: {
            validator: function (value, param) {
                if (value == "") {
                    return true;
                }
                var reg = /^\d{4}[-](0?[1-9]|1[0-2])[-](0?[1-9]|[12][0-9]|[3][01])?$/;
                if (value.match(reg) == null) {
                    $.fn.validatebox.defaults.rules.dateboxformat.message = "有效日期格式如：2015-02-13,2015-2-3";
                    return false;
                }
                else {
                    var date = $.fn.datebox.defaults.parser(value);
                    var y = date.getFullYear();
                    var m = date.getMonth() + 1;
                    var d = date.getDate();

                    var transferDatestr = y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);

                    var thisValueArr = (value.split('-'));
                    var thisValueY = parseInt(thisValueArr[0], 10);
                    var thisValueM = parseInt(thisValueArr[1], 10);
                    var thisValueD = parseInt(thisValueArr[2], 10);
                    var thisValue = thisValueY + '-' + (thisValueM < 10 ? ('0' + thisValueM) : thisValueM) + '-' + (thisValueD < 10 ? ('0' + thisValueD) : thisValueD);

                    if (thisValue == transferDatestr) {
                        return true;
                    }
                    else {
                        $.fn.validatebox.defaults.rules.dateboxformat.message = "无效日期格式如：2015-15-30,2015-2-29";
                        return false;
                    }
                    return true;
                }
            },
            message: ""
        },
        datetimeboxformat: {
            validator: function (value, param) {
                if (value == "") {
                    return true;
                }
                var reg = /^\d{4}[-](0?[1-9]|1[0-2])[-](0?[1-9]|[12][0-9]|[3][01]) (0?[0-9]|[1-5]?[0-9]):(0?[0-9]|[1-5]?[0-9])$/;
                if (value.match(reg) == null && value.match(/^\d{4}[-](0?[1-9]|1[0-2])[-](0?[1-9]|[12][0-9]|[3][01]) (0?[0-9]|[1-5]?[0-9]):(0?[0-9]|[1-5]?[0-9]):(0?[0-9]|[1-5]?[0-9])$/) == null) {
                    $.fn.validatebox.defaults.rules.datetimeboxformat.message = "有效日期格式如：2015-02-13 11:31 ,2015-2-3 1:03";
                    return false;
                }
                else {//以下为年月日的校验
                    var date = $.fn.datebox.defaults.parser(value);
                    var y = date.getFullYear();
                    var m = date.getMonth() + 1;
                    var d = date.getDate();

                    var transferDatestr = y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);

                    var thisValueArr = (value.split('-'));
                    var thisValueY = parseInt(thisValueArr[0], 10);
                    var thisValueM = parseInt(thisValueArr[1], 10);
                    var thisValueD = parseInt(thisValueArr[2], 10);
                    var thisValue = thisValueY + '-' + (thisValueM < 10 ? ('0' + thisValueM) : thisValueM) + '-' + (thisValueD < 10 ? ('0' + thisValueD) : thisValueD);

                    if (thisValue == transferDatestr) {
                        return true;
                    }
                    else {
                        $.fn.validatebox.defaults.rules.dateboxformat.message = "无效日期格式如：2015-15-30 00:00,2015-2-29 00:00";
                        return false;
                    }
                    return true;
                }
            },
            message: ""
        },
        factory: {
            validator: function (value, param) {
                if (param.length <= 2) {
                    $.fn.validatebox.defaults.rules.factory.message = param[1];
                    return param[0](value);
                }
                else {
                    var paramInfact = [];
                    for (var i = 1; i < param.length; i++) {
                        paramInfact.push(param[i]);
                    }
                    return param[0](value, paramInfact);
                }
            },
            message: ""
        },
        dateEarlier: {
            validator: function (value, param) {
                var compareValue = $("#" + param[0]).datebox('getValue');
                if (compareValue == "" || value == "") {
                    return true;
                }
                if (param[1] == undefined || !param[1]) {
                    $.fn.validatebox.defaults.rules.dateEarlier.message = String.format(CommonResources.ERR_MSG_NOT_LATER, CommonResources.STR_ALERT_MIN_CREATION_DATE, CommonResources.STR_ALERT_MAX_CREATION_DATE);
                }
                else {
                    $.fn.validatebox.defaults.rules.dateEarlier.message = param[1];
                }
                var compareDate = $.fn.datebox.defaults.parser(compareValue);
                var thisDate = $.fn.datebox.defaults.parser(value);

                return thisDate <= compareDate;
            },
            message: ""
        },
        dateLaterThan: {
            validator: function (value, param) {
                var compareValue = $("#" + param[0]).datebox('getValue');
                if (compareValue == "" || value == "") {
                    return true;
                }
                if (param[1] == undefined || !param[1]) {
                    $.fn.validatebox.defaults.rules.dateLaterThan.message = String.format(CommonResources.ERR_MSG_NOT_EARLIER, CommonResources.STR_ALERT_MAX_CREATION_DATE, CommonResources.STR_ALERT_MIN_CREATION_DATE);
                }
                else {
                    $.fn.validatebox.defaults.rules.dateLaterThan.message = param[1];
                }
                var compareDate = $.fn.datebox.defaults.parser(compareValue);
                var thisDate = $.fn.datebox.defaults.parser(value);

                return thisDate >= compareDate;
            },
            message: ""
        }
    });
})(jQuery);
/***************datagrid**********************************/
(function ($) {
    $.extend($.fn.datagrid.methods, {
        /**
        * Datagrid扩展方法tooltip 基于Easyui 1.3.3，可用于Easyui1.3.3+
        * 简单实现，如需高级功能，可以自由修改
        * 使用说明:
        *   在easyui.min.js之后导入本js
        *   代码案例:
        *      $("#dg").datagrid({....}).datagrid('tooltip'); 所有列
        *      $("#dg").datagrid({....}).datagrid('tooltip',{fields:['productid','listprice']}); 指定列,指定位置
        */
        tooltip: function (jq, param) {
            return jq.each(function () {
                var grid = $(this);
                var panel = $(this).datagrid('getPanel');
                var header = panel.find("div.datagrid-header");
                var table = header.find("table.datagrid-htable");
                var tr = table.find("tr.datagrid-header-row");
                if (param && param.fields && typeof param.fields == 'object' && param.fields.sort) {
                    $.each(param.fields, function () {
                        var field = this;
                        var opts = grid.datagrid('getColumnOption', field);
                        var fullName = opts.fullName;
                        var position = opts.pos;
                        var selector = "td[field='" + field + "']";
                        var td = tr.find(selector);
                        bindEvent(td, fullName, position);
                    });
                } else {
                    var tds = tr.find("td");
                    tds.each(function (i) {
                        var fullName;
                        var position = "top";
                        var field = $(this).attr('field');
                        if (field != undefined && field.length > 0) {
                            var opts = grid.datagrid('getColumnOption', field);
                            fullName = opts.fullName;
                            position = opts.pos;
                        }
                        bindEvent($(this), fullName, position);
                    });
                }
            });

            function bindEvent(jqs, content, pos) {
                if (pos == undefined)
                    pos = "top";
                jqs.mouseover(function () {
                    //var content = $.trim($(this).text());
                    if (content == undefined || content.length == 0) {
                        content = $.trim($(this).text());
                    }
                    if (content.length > 0) {
                        $(this).tooltip({
                            position: pos,
                            content: content,
                            trackMouse: true,
                            onHide: function () {
                                $(this).tooltip('destroy');
                            }
                        }).tooltip('show');
                    }
                });
            }
        },
        /*可以在指定的datagrid表头实现tooltip效果,需要在指定列上加上fullName属性*/
        addHeaderTip: function (jq, params) {
            function showTip(showParams, td, e, dg) {
                //无文本，不提示。      
                if ($(td).text() == "") return;

                params = params || {
                };
                var options = dg.data('datagrid');
                showParams.content = '<div class="tipcontent">' + showParams.content + '</div>';
                $(td).tooltip({
                    content: showParams.content,
                    trackMouse: true,
                    position: params.position,
                    onHide: function () {
                        $(this).tooltip('destroy');
                    },
                    onShow: function () {
                        var tip = $(this).tooltip('tip');
                        if (showParams.tipStyler) {
                            tip.css(showParams.tipStyler);
                        }
                        if (showParams.contentStyler) {
                            tip.find('div.tipcontent').css(showParams.contentStyler);
                        }
                    }
                }).tooltip('show');
            };
            return jq.each(function () {
                var grid = $(this);
                var options = $(this).data('datagrid');
                if (!options.tooltip) {
                    var panel = grid.datagrid('getPanel').panel('panel');
                    panel.find('.datagrid-header').each(function () {
                        var delegateEle = $(this).find('> div.datagrid-header-inner').length ? $(this).find('> div.datagrid-header-inner')[0] : this;
                        $(delegateEle).undelegate('td', 'mouseover').undelegate('td', 'mouseout').undelegate('td', 'mousemove').delegate('td[field]', {
                            'mouseover': function (e) {
                                var fullName = grid.datagrid('getColumnOption', $(this).attr('field')).fullName;
                                if (fullName === undefined) return;
                                var that = this;
                                options.factContent = $(this).find('>div').clone().css({ 'margin-left': '-5000px', 'width': 'auto', 'display': 'inline', 'position': 'absolute' }).appendTo('body');
                                var factContentWidth = options.factContent.width();
                                params.content = fullName;
                                if (params.onlyShowInterrupt) {
                                    if (factContentWidth > $(this).width()) {
                                        showTip(params, this, e, grid);
                                    }
                                } else {
                                    showTip(params, this, e, grid);
                                }
                            },
                            'mouseout': function (e) {
                                if (options.factContent) {
                                    options.factContent.remove();
                                    options.factContent = null;
                                }
                            }
                        });
                    });
                }
            });
        }
    });
})(jQuery);
(function ($) {
    /**
    * 针对panel window dialog三个组件拖动时会超出父级元素的修正
    * 如果父级元素的overflow属性为hidden，则修复上下左右个方向
    * 如果父级元素的overflow属性为非hidden，则只修复上左两个方向
    * @param left
    * @param top
    * @returns
    */
    var easyuiPanelOnMove = function (left, top) {
        $('.window-shadow').css('display', 'none');

        if (left < 0) {
            $(this).panel('move', {
                left: 0
            });
        }
        if (top < 0) {
            $(this).panel('move', {
                top: 0
            });
        }

        var maxWidth = window.screen.availWidth - $(this).panel('options').width - 5;
        if (left > maxWidth) {
            $(this).panel('move', {
                left: maxWidth
            });
        }

        var maxHeight = window.screen.availHeight - $(this).panel('options').height - 5;
        if (top > maxHeight) {
            $(this).panel('move', {
                top: maxHeight
            });
        }
    };

    $.fn.window.defaults.onMove = easyuiPanelOnMove;
    $.fn.panel.defaults.onMove = easyuiPanelOnMove;
    $.fn.dialog.defaults.onMove = easyuiPanelOnMove;

    $.extend($.messager.defaults, { ok: "确认", cancel : "取消" });

    $.fn.pagination.defaults.beforePageText = '第';
    $.fn.pagination.defaults.afterPageText = '页 共{pages}页';
    $.fn.pagination.defaults.displayMsg = '当前显示 {from} - {to} 条记录   共{total} 条记录';

    $.fn.validatebox.defaults.missingMessage = '该输入项为必输项';
    $.fn.validatebox.defaults.rules.email.message = '请输入有效的电子邮件地址';
    $.fn.validatebox.defaults.rules.url.message = '请输入有效的URL地址';
    $.fn.validatebox.defaults.rules.length.message = '输入内容长度必须介于{0}和{1}之间';
    $.fn.validatebox.defaults.rules.remote.message = '请修正该字段';
    $.fn.validatebox.defaults.rules.invalidMessage = '请选择正确项';
    $.fn.validatebox.defaults.rules.numberMessage = '此项必须为数字!';
    $.fn.validatebox.defaults.rules.ipMessage = '请输入正确的IP，例如192.168.0.1';
    $.fn.validatebox.defaults.rules.telephoneMessage = '请输入正确的电话号码';

    $.fn.panel.defaults.loadingMessage = '正在处理，请稍候。。。';

    $.fn.datagrid.defaults.loadMsg = '正在处理，请稍候。。。';
    $.fn.treegrid.defaults.loadMsg = '正在处理，请稍候。。。';

    $.fn.calendar.defaults.weeks = ['日', '一',
                                    '二', '三',
                                    '四', '五',
                                    '六'];
    $.fn.calendar.defaults.months = ['一月', '二月',
                                     '三月', '四月',
                                     '五月', '六月',
                                     '七月', '八月',
                                     '九月', '十月',
                                     '十一月', '十二月'];

    $.fn.datebox.defaults.currentText = '今天';
    $.fn.datebox.defaults.closeText = '关闭';
    $.fn.datebox.defaults.okText = '确认';
    $.fn.datebox.defaults.missingMessage = '该输入项为必输项';

    $.fn.datetimebox.defaults.currentText = '今天';
    $.fn.datetimebox.defaults.closeText = '关闭';
    $.fn.datetimebox.defaults.okText = '确认';
    $.fn.datetimebox.defaults.missingMessage = '该输入项为必输项';

    $.fn.combobox.defaults.missingMessage = '该输入项为必输项';
    $.fn.combotree.defaults.missingMessage = '该输入项为必输项';
    $.fn.combogrid.defaults.missingMessage = '该输入项为必输项';
    $.fn.numberbox.defaults.missingMessage = '该输入项为必输项';
})(jQuery);
/***************calendar datebox  datetime****************/
(function ($) {
    $.fn.datebox.defaults.buttons.push({
        text: function (_9461) {
            return $(_9461).datebox("options").cleanText;
        }, handler: function (_9462) {
            $(_9462).combo('setValue', '').combo('setText', '');
            $(this).closest("div.combo-panel").panel("close");
        }
    });
    $.fn.datetimebox.defaults.buttons.push({
        text: function (_9661) {
            return $(_9661).datebox("options").cleanText;
        }, handler: function (_9662) {
            $(_9662).combo('setValue', '').combo('setText', '');
            $(this).closest("div.combo-panel").panel("close");
        }
    });

    $.extend($.fn.calendar.defaults, {
        showNow: true,
        showAfterNow: true,
        showBeforeNow: true,
        validator: function (date) {
            var newDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            var now = new Date();
            var nowDate = new Date(now.getFullYear(), now.getMonth(), now.getDate());

            var opts = $(this).calendar("options");
            if (opts.showAfterNow == false) {
                if (newDate > nowDate) {
                    return false;
                }
            }
            if (opts.showBeforeNow == false) {
                if (newDate < nowDate) {
                    return false;
                }
            }
            if (opts.showNow == false) {
                if (newDate.toString() == nowDate.toString()) {
                    return false;
                }
            }
            return true;
        }
    });

    $.extend($.fn.datebox.methods, {
        calendarOptions: function (jq) {
            var opts = $(jq).datebox('options');
            return {
                showNow: opts.showNow,
                showAfterNow: opts.showAfterNow,
                showBeforeNow: opts.showBeforeNow
            };
        }
    });

    $.extend($.fn.datebox.defaults, {
        showNow: true,
        showAfterNow: true,
        showBeforeNow: true,
        cleanText: "清空",
        formatter: function (date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
        },
        parser: function parseDate(dateString) {
            if (!dateString) return new Date();
            var ss = (dateString.split('-'));
            var y = parseInt(ss[0], 10);
            var m = parseInt(ss[1], 10);
            var d = parseInt(ss[2], 10);
            if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                return new Date(y, m - 1, d);
            } else {
                return new Date();
            }
        }
    });

    $.extend($.fn.datetimebox.defaults, {
        parser: function (strDate) {
            if (!strDate) return new Date();
            var st = strDate;
            var a = st.split(" ");
            var b = a[0].split("/");
            if (b.length == 1) {
                b = a[0].split("-");
            }
            if (a.length == 1) {
                var y = parseInt(b[0], 10);
                var m = parseInt(b[1], 10);
                var d = parseInt(b[2], 10);
                if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {

                    return new Date(y, m - 1, d);
                }
                else {
                    return new Date();
                }
            }
            else {
                var c = a[1].split(":");
                var y = parseInt(b[0], 10);
                var m = parseInt(b[1], 10);
                var d = parseInt(b[2], 10);
                var hh = parseInt(c[0], 10);
                var mm = parseInt(c[1], 10);
                var ss = parseInt(c[2], 10);
                if (!isNaN(y) && !isNaN(m) && !isNaN(d) && !isNaN(hh) && !isNaN(mm) && !isNaN(ss)) {
                    var date = new Date(y, m - 1, d, hh, mm, ss)
                    return date;
                }
                else if (!isNaN(y) && !isNaN(m) && !isNaN(d) && !isNaN(hh) && !isNaN(mm)) {
                    var date = new Date(y, m - 1, d, hh, mm)
                    return date;
                }
                else if (!isNaN(y) && !isNaN(m) && !isNaN(d) && !isNaN(hh)) {
                    var date = new Date(y, m - 1, d, hh, 0)
                    return date;
                }
                else if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                    var date = new Date(y, m - 1, d);
                    return date;
                }
                else {
                    return new Date();
                }
            }
        }
    });
})(jQuery);
/***************linkbutton********************************/
(function ($) {
    $.extend($.fn.linkbutton.methods, {
        setText: function (jq, param) {
            if (param.text) {
                jq.find(".l-btn-text").html(param.text);
            }
            if (param.iconCls) {
                $(jq.find(".l-btn-icon")).addClass(param.iconCls);
            }
        }
    });
})(jQuery);
/****************combo、combobox************************************/
(function ($) {
    $.extend($.fn.combo.defaults, {
        required: true
    });
    $.extend($.fn.combobox.defaults, {
        required: true
    });

    $.extend($.fn.combobox.methods, {
        cascadeDisable: function (jq, options) {
            jq.combobox('clear');
            jq.combobox('disable');
            var nextOptions = options.options;
            if (nextOptions) {
                $("#" + nextOptions.id).combobox('cascadeDisable', nextOptions);
            }
        },
        cascadeShow: function (jq, options) {
            jq.each(function () {
                $(this).combobox({
                    url: options.url,
                    valueField: 'id',
                    textField: 'text',
                    queryParams: { paramId: options.paramId, selectedValue: options.selectedValue },
                    editable: false,
                    onSelect: function (record) {
                        var nextOptions = options.options;
                        if (nextOptions) {
                            nextOptions.paramId = record.id;
                            $("#" + nextOptions.id).combobox('cascadeShow', nextOptions);
                        }
                    },
                    onLoadSuccess: function (result) {
                        if (result.length > 0) {
                            $(this).combobox('enable');
                            var nextOptions = options.options;
                            if (nextOptions) {
                                nextOptions.paramId = $(this).combobox('getValue');
                                $("#" +nextOptions.id).combobox('cascadeShow', nextOptions);
                            }
                        }
                        else {
                            $(this).combobox('cascadeDisable', options);
                        }
                    }
                });
            });
        }
    });
})(jQuery);
/****************combotree********************************/
(function ($) {

    function getText(tree, roots, newTextArray) {
        if (roots.length > 0) {
            for (var j = 0; j < roots.length; j++) {
                var pnode = tree.tree("getNode", roots[j].target);
                if (pnode != null) {
                    if (pnode.checked) {
                        //如果根节点已选中，则只显示根节点的文本
                        newTextArray.push(pnode.text);
                    } else {
                        //否则只显示子节点文本
                        //var children = tree.tree("getChildren", pnode.target);
                        getText(tree, pnode.children, newTextArray);
                    }
                }
            }
        }
        return newTextArray;
    }

    function setText(target) {
        var opts = $.data(target, "combotree").options;
        var tree = $.data(target, "combotree").tree;
        var roots = tree.tree("getRoots");
        var aa = getText(tree, roots, []);
        $(target).combo("setText", aa.join(opts.separator));
    }
    
    $.extend($.fn.combotree.methods, {
        show: function (jq, params) {
            jq.each(function (index, e) {
                var eTarget = e;
                $(e).combotree({
                    url: params.url,
                    required: params.required ? params.required : true,
                    multiple: params.multiple ? params.multiple : true,
                    onlyLeafCheck: params.onlyLeafCheck ? params.onlyLeafCheck : false,//Defines if to show the checkbox only before leaf node.
                    lines: true,
                    onLoadSuccess: function (node, data) {
                        var opts = $(eTarget).combo("options");
                        opts.originalValue = $(eTarget).combotree('getValues');

                        setText(eTarget);
                    },
                    onClick: function (node) {
                        //此时获取到的combotree是选中或未选中后的结果。
                        setText(eTarget);
                    },
                    onHidePanel: function () {
                        var tree = $(this).combotree('tree');
                        var checkedNodes = tree.tree('getChecked');
                        if (checkedNodes.length == 0) {
                            var opts = $(this).combotree("options");
                            $(this).combotree('setValue', opts.originalValue);
                            setText(this);
                        }
                    }
                });
            });
        },
        reset: function (jq) {
            jq.each(function (index, e) {
                var opts = $.data(e, "combotree").options;
                $(e).combotree('setValue', opts.originalValue);
                setText(e);
            });
        }
    });
})(jQuery);