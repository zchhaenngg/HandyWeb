import { IModalElement } from './hy_bootstrap_modal.d';
import { Ihy_modal } from './hy_bootstrap_modal.d';
import { bs_modal } from './hy_bootstrap_modal.d';
class ModalElement implements IModalElement {
    modal: HTMLDivElement;
    modal_diag: HTMLDivElement;
    modal_content: HTMLDivElement;
    header: HTMLDivElement;
    body: HTMLDivElement;
    footer: HTMLDivElement;
    buttons: HTMLButtonElement[];

    constructor() {
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

    addBtn(btnText: string) {
        var newButton = document.createElement("button");
        newButton.innerHTML = btnText;
        this.buttons.push(newButton);
        this.footer.appendChild(newButton);
        return newButton;
    }
    setHeaderHtml(innerHtml: string) {
        this.header.innerHTML = innerHtml;
    }
    setBodyHtml(innerHtml: string) {
        this.body.innerHTML = innerHtml;
    }
}

class hy_modal implements Ihy_modal {
    private _modalElement: ModalElement;
    
    show_bs_modal?: (relatedTarget?: any) => boolean;
    shown_bs_modal?: (relatedTarget?: any) => void;
    hide_bs_modal?: (relatedTarget?: any) => boolean
    hidden_bs_modal?: (relatedTarget?: any) => void;
    
    constructor(bodyHtml: string) {
        this.createElement();
        this._modalElement.setBodyHtml(bodyHtml);
    }

    private get $modal(): JQuery {
        return $(this._modalElement.modal);
    }

    private get bs_modal(): bs_modal {
        
        return this.$modal.data('bs.modal') as bs_modal;
    }
    
    private createElement() {
        this._modalElement = new ModalElement();
        $(document.body).append(this.$modal);
        this.$modal.modal({ show: false });
        return this;
    }
    setHeaderHtml(innerHtml: string): hy_modal {
        this._modalElement.setHeaderHtml(innerHtml);
        return this;
    }
    setBodyHtml(innerHtml: string): hy_modal {
        this._modalElement.setBodyHtml(innerHtml);
        return this;
    }

    addButton(btnMessage: string = '关闭', action?: Function) {
        var button = this._modalElement.addBtn(btnMessage);
        var modal = this;
        button.addEventListener('click', function () {
            if (action) {
                action.call(modal, this);
            } else {
                modal.hide();//如果没有事件则默认为关闭按钮事件
            }
        });
        return this;
    }
    
    get isShown(): boolean {
        return this.bs_modal.isShown;
    }

    show(relatedTarget?: any): void {
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
    }

    hide(relatedTarget?: any): void {
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
        
    }
}