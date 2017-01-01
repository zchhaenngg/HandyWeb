"use strict";
var ModalElement = (function () {
    function ModalElement() {
        this.modal = document.createElement("div");
        this.modal_diag = document.createElement("div");
        this.modal_content = document.createElement("div");
        this.header = document.createElement("div");
        this.body = document.createElement("div");
        this.footer = document.createElement("div");
        this.buttons = [];
        this.modal.className = "modal";
        this.modal_diag.className = "modal-dialog";
        this.modal_content.className = "modal-content";
        this.header.className = "modal-header";
        this.body.className = "modal-body";
        this.footer.className = "modal-footer";
        this.modal.appendChild(this.modal_diag);
        this.modal_diag.appendChild(this.modal_content);
        this.modal_content.appendChild(this.header);
        this.modal_content.appendChild(this.body);
        this.modal_content.appendChild(this.footer);
    }
    ModalElement.prototype.addBtn = function (btnText) {
        var newButton = document.createElement("button");
        newButton.innerHTML = btnText;
        this.buttons.push(newButton);
        this.footer.appendChild(newButton);
        return newButton;
    };
    ModalElement.prototype.setHeaderHtml = function (innerHtml) {
        this.header.innerHTML = innerHtml;
    };
    ModalElement.prototype.setBodyHtml = function (innerHtml) {
        this.body.innerHTML = innerHtml;
    };
    return ModalElement;
}());
var hy_modal = (function () {
    function hy_modal(bodyHtml) {
        this.createElement();
        this._modalElement.setBodyHtml(bodyHtml);
    }
    Object.defineProperty(hy_modal.prototype, "$modal", {
        get: function () {
            return $(this._modalElement.modal);
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(hy_modal.prototype, "bs_modal", {
        get: function () {
            return this.$modal.data('bs.modal');
        },
        enumerable: true,
        configurable: true
    });
    hy_modal.prototype.createElement = function () {
        this._modalElement = new ModalElement();
        $(document.body).append(this.$modal);
        this.$modal.modal({ show: false });
        return this;
    };
    hy_modal.prototype.setHeaderHtml = function (innerHtml) {
        this._modalElement.setHeaderHtml(innerHtml);
        return this;
    };
    hy_modal.prototype.setBodyHtml = function (innerHtml) {
        this._modalElement.setBodyHtml(innerHtml);
        return this;
    };
    hy_modal.prototype.addButton = function (btnMessage, action) {
        if (btnMessage === void 0) { btnMessage = '关闭'; }
        var button = this._modalElement.addBtn(btnMessage);
        var modal = this;
        button.addEventListener('click', function () {
            if (action) {
                action.call(modal, this);
            }
            else {
                modal.hide(); //如果没有事件则默认为关闭按钮事件
            }
        });
        return this;
    };
    Object.defineProperty(hy_modal.prototype, "isShown", {
        get: function () {
            return this.bs_modal.isShown;
        },
        enumerable: true,
        configurable: true
    });
    hy_modal.prototype.show = function (relatedTarget) {
        var isShow = true;
        if (this.show_bs_modal) {
            isShow = this.show_bs_modal(relatedTarget);
        }
        if (isShow) {
            this.bs_modal.show(relatedTarget);
            if (this.shown_bs_modal) {
                this.shown_bs_modal(relatedTarget);
            }
        }
    };
    hy_modal.prototype.hide = function (relatedTarget) {
        var isHide = true;
        if (this.hide_bs_modal) {
            isHide = this.hide_bs_modal(relatedTarget);
        }
        if (isHide) {
            this.bs_modal.hide(relatedTarget);
            if (this.hidden_bs_modal) {
                this.hidden_bs_modal(relatedTarget);
            }
        }
    };
    return hy_modal;
}());
//# sourceMappingURL=hy_bootstrap_modal.js.map