String.format = function () {
    if (arguments.length == 0)
        return null;

    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'igm');
        str = str.replace(re, arguments[i]);
    }
    return str;
};

//全屏遮罩
function LockMainPage() {
    $('#div_freeze_all').show();
}
//全屏失去遮罩
function UnLockMainPage() {
    $('#div_freeze_all').hide();
}

function getAutoHeight(height) {
    var screenHeight = window.screen.height;
    if (screenHeight && screenHeight < 1000) {
        height = height > 700 ? (height * 0.8) : height;
    }
    return height;
}

function adjustSize(body_container_id) {
    
}

//设置可伸缩
function makeExpandOrCollapseWithDbClick() {
    var panels = $(".easyui-panel.collapsible");
    panels.each(function (index, e) {
        var $panel = $(this);
        var opts = $panel.panel("options");
        opts.headerCls = 'hand';
        //$(this).panel(opts);
        var pHeader = $panel.panel("header");
        pHeader.bind("click", function () {
            var collapsed = opts.collapsed;
            if (collapsed) {
                $panel.panel('expand', true);
            } else {
                $panel.panel('collapse', true);
            }
        });
    });
}


//function datagrid4findHistory(divId, modelId, historyFlag) {
//    var descriptionWidth = $('#' + divId).width() - 430;
//    $('#' + divId).datagrid({
//        singleSelect: true,
//        url: String.format('/Common/JsonFindHistory/{0}?HistoryFlag={1}', modelId, historyFlag),
//        loadMsg: CommonResources.STR_LOADING,
//        pagination: true,
//        pageSize: 10,
//        pageList: [10, 20, 30],
//        sortOrder: 'asc',
//        rownumbers: true,
//        nowrap: false,
//        //fitColumns: true,
//        columns: [
//            [
//                { title: CommonResources.STR_CREATED_DATE, field: 'CreationDateStr', width: 160, align: 'center', sortable: true },
//                { title: CommonResources.STR_OPERATOR, field: 'CreatedUserStr', align: 'center', width: 80, sortable: true },
//                { title: CommonResources.STR_ACTION_ITEM, field: 'OperateString', width: 135, halign: 'center',sortable: true },
//                { title: CommonResources.STR_COMMENT, field: 'Description', align: 'left', width: descriptionWidth }
//            ]
//        ],
//        onLoadSuccess: function (data) {
//            if (data.IsSuccess == false) {
//                $.messager.alert(CommonResources.STR_ACTION_FAIL, data.Message, 'warning');
//                return;
//            }
//        }
//    });
//}




var toolbarEx = {
    create: function (handler, text, options) {
        text = text || "新建";
        return $.extend({ iconCls: 'icon-add', text: text, handler: handler }, options);
    },
    edit: function (handler, text, iconCls, options) {
        text = text || "编辑";
        iconCls = iconCls || "icon-edit";
        return $.extend({
            iconCls: iconCls,
            text: text,
            handler: handler,
            rowOnSelect: function (rowIndex, rowData) {
                this.linkbutton("enable");
            },
            onLoadSuccess: function (data) {
                this.linkbutton('disable');
            }
        }, options);
    },
   
    /**
     *  适用于互斥性质的如 启用-失效 组合linkbutton
     *  mutexOption 对象如 
                var mutexOption = {
                    Valid: { text: "启用", iconCls: "icon-activate" },
                    InValid: { text: "失效", iconCls: "icon-inactivate" }
                };
     *  function (rowData) { return rowData.IsValid; }
     *  ajaxExConfirmHandler  函数需要返回ajaxEx.confirm的所有参数组合而成的对象
     */
    mutex: function (mutexOption, ajaxExConfirmHandler, rowValidHandler) {
       
        function handler() {
            var $button = $(this);
            var confirmParams = ajaxExConfirmHandler();

            ajaxEx.confirm(confirmParams.confirmMsg, confirmParams.url, confirmParams.params, function () {
                confirmParams.onSuccess();
                $button.linkbutton('setText', mutexOption.Valid);
            }, confirmParams.onFail);
        }

        return toolbarEx.edit(handler, mutexOption.Valid.text, mutexOption.Valid.iconCls, {
            rowOnSelect: function (rowIndex, rowData) {
                this.linkbutton("enable");
                var isValid = rowValidHandler(rowData);
                if (isValid) {
                    this.linkbutton('setText', mutexOption.InValid);
                } else {
                    this.linkbutton('setText', mutexOption.Valid);
                }
            }
        });
    },
    getAll: function (btns) {
        function addbutton(button) {
            if (lastButton) {
                toolbar.push('-');
            }
            if (button) {//不为null,""，undefined时才认为是valid button
                toolbar.push(button);
                lastButton = button;
            }
        }

        var toolbar = [];
        var lastButton = null;
        if ($.isArray(btns)) {
            for (var i = 0; i < btns.length; i++) {
                addbutton(btns[i]);
            }
        }
        else {
            addbutton(btns);
        }
        return toolbar;
    }
}
var diagEx = {
    openDiag:function (options) {
        var dialogOptions = $.extend({
            loadingMessage: CommonResources.STR_LOADING,
            collapsible: false,
            minimizable: false,
            maximizable: false,
            resizable: false,
            autoOpen: false,
            cache: false,
            iconCls: 'icon-add',
            modal: true,
            shadow: true,
            closed: true
        }, {
            title: options.title,
            width: options.width,
            height: options.height,
            buttons: options.buttons,
            iconCls: options.iconCls ? options.iconCls : 'icon-add'
        });
        if (options.iconCls) {
            dialogOptions.iconCls = options.iconCls;
        }
        $("#" + options.diagId).dialog(dialogOptions);
        $("#" + options.diagId).dialog('open').dialog('refresh', options.href);
    },
    submitClose: function (href, title, width, height, options) {

        function openDiagt(options) {
            var buttons = [
                {
                    text: options.SubmitStr == undefined ? CommonResources.STR_SUBMIT : options.SubmitStr,
                    size: 'large',
                    iconCls: 'icon-large-submit',
                    iconAlign: 'left',
                    handler: function () {
                        diag_submit($diag, this);
                    }
                },
                {
                    text: CommonResources.STR_CLOSE,
                    size: 'large',
                    iconCls: 'icon-large-close',
                    iconAlign: 'left',
                    handler: function () {
                        $diag.dialog("close");
                    }
                }
            ];
            var $diag = $("#" + options.diagId);
            diagEx.openDiag({
                diagId: options.diagId,
                title: options.title,
                width: options.width,
                height: options.height,
                buttons: buttons,
                href: options.href,
                iconCls: options.iconCls
            });
        }

        options = options || {};
        var opts = $.extend({ diagId: 'div_diag', title: title, width: width, height: height, href: href }, options);
        openDiagt(opts);
    },
    submitSaveClose: function (href, title, width, height, options) {
        function openDiagt(options) {
            var buttons = [
                {
                    text: options.SaveStr == undefined ? CommonResources.STR_SAVE : options.SaveStr,
                    size: 'large',
                    iconCls: 'icon-large-save',
                    iconAlign: 'left',
                    handler: function () {
                        diag_save($diag, this);
                    }
                },
                {
                    text: options.SubmitStr == undefined ? CommonResources.STR_SUBMIT : options.SubmitStr,
                    size: 'large',
                    iconCls: 'icon-large-submit',
                    iconAlign: 'left',
                    handler: function () {
                        diag_submit($diag, this);
                    }
                },
                {
                    text: CommonResources.STR_CLOSE,
                    size: 'large',
                    iconCls: 'icon-large-close',
                    iconAlign: 'left',
                    handler: function () {
                        $diag.dialog("close");
                    }
                }
            ];
            var $diag = $("#" + options.diagId);
            diagEx.openDiag({
                diagId: options.diagId,
                title: options.title,
                width: options.width,
                height: options.height,
                buttons: buttons,
                href: options.href,
                iconCls: options.iconCls
            });
        }

        options = options || {};
        var finalOptions = $.extend({ diagId: 'div_diag', title: title, width: width, height: height, href: href }, options);

        openDiagt(finalOptions);
    },
    close: function (href, title, width, height, options) {
        function openDiagt(options) {
            var buttons = [{
                text: CommonResources.STR_CLOSE,
                size: 'large',
                iconCls: 'icon-large-close',
                iconAlign: 'left',
                handler: function () {
                    $diag.dialog("close");
                }
            }];
            var $diag = $("#" + options.diagId);
            diagEx.openDiag({
                diagId: options.diagId,
                title: options.title,
                width: options.width,
                height: options.height,
                buttons: buttons,
                href: options.href,
                iconCls: options.iconCls
            });
        }

        options = options || {};
        var finalOptions = $.extend({ diagId: 'div_diag', title: title, width: width, height: height, href: href }, options);
        openDiagt(finalOptions);
    }
}

function checkResize() {
    adjustSize('body_container');
}

function validate(target, isValidateCode) {
    if ($.fn.validatebox) {
        var t = $(target);
        t.find('.validatebox-text:not(:disabled)').validatebox('validate', isValidateCode);
        var invalidbox = t.find('.validatebox-invalid');
        invalidbox.filter(':not(:disabled):first').focus();
        return invalidbox.length == 0;
    }
    return true;
}

//经常直接使用的
function fillSize(percent) {
    //var gCenter = $('div#body_container');
    //var oldPanelWidth = gCenter.panel('panel').outerWidth();
    //return (oldPanelWidth - 100) * percent;
    var width = $('div#body_container').innerWidth();
    return (width - 100) * percent;
}

function IsValidNumber(value, min, max, isMinEqual, isMaxEqual) {
    if (!(/^(\d*|(\d*\.\d*))$/.test(value))) {
        $.fn.validatebox.defaults.rules.factory.message = "输入内容必须为数字格式";
        return false;
    }
    var valueNum = parseFloat(value);
    if (isMinEqual == undefined || isMinEqual) {
        if (min) {
            var isValid = valueNum >= min;
            if (!isValid) {
                $.fn.validatebox.defaults.rules.factory.message = "输入内容必须大于等于" + min;
                return false;
            }
        }
    }
    else {
        if (min || min == 0) {
            var isValid = valueNum > min;
            if (!isValid) {
                $.fn.validatebox.defaults.rules.factory.message = "输入内容必须大于" + min;
                return false;
            }
        }
    }
    if (isMaxEqual == undefined || isMaxEqual) {
        if (max || max == 0) {
            var isValid = valueNum <= max;
            if (!isValid) {
                $.fn.validatebox.defaults.rules.factory.message = "输入内容必须小于等于" + max;
                return false;
            }
        }
    }
    else {
        if (max || max == 0) {
            var isValid = valueNum < max;
            if (!isValid) {
                $.fn.validatebox.defaults.rules.factory.message = "输入内容必须小于" + max;
                return false;
            }
        }
    }
    return true;
}

var ajaxEx = {
    defaults: function (url, params, onSuccess, onFail) {
        LockMainPage();
        $.ajax({
            type: "POST",
            async: false,
            dataType: "json",
            url: url,
            data: params,
            success: function (data) {
                if (data.IsSuccess) {
                    if (onSuccess) {
                        onSuccess.call();
                    }
                    $.messager.show({
                        title: CommonResources.STR_ACTION_SUCCESS,
                        msg: data.Message,
                        timeout: 3000
                    });
                }
                else {
                    if (onFail) {
                        onFail.call();
                    }
                    $.messager.alert('操作失败', data.Message);
                }
                UnLockMainPage();
            },
            error: function (xmlHttpRequest, texStatus, errorThrown) {
                $.messager.alert('error', errorThrown);
                UnLockMainPage();
            }
        });
    },
    confirm: function (confirmMsg, url, params, onSuccess, onFail) {
        $.messager.confirm('确认', confirmMsg, function (r) {
            if (r) {
                ajaxEx.defaults(url, params, onSuccess, onFail);
            }
        });
    }
};