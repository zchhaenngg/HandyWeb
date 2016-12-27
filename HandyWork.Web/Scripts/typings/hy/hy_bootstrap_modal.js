class ModalElement {
    constructor() {
        this.header = document.createElement("div");
        this.body = document.createElement("div");
        this.footer = document.createElement("div");
        this.buttons = [];
        this.header.className = "modal-header";
        this.body.className = "modal-body";
        this.footer.className = "modal-footer";
    }
    addBtn(btnText) {
        var newButton = document.createElement("button");
        newButton.innerHTML = btnText;
        this.buttons.push(newButton);
        this.footer.appendChild(newButton);
        return newButton;
    }
    setMessage(msg) {
        this.body.innerHTML = msg;
    }
    getElement() {
        var modalEl = document.createElement("div");
        modalEl.className = "modal";
        if (this.header.innerHTML) {
            modalEl.appendChild(this.header);
        }
        modalEl.appendChild(this.body);
        modalEl.appendChild(this.footer);
        return modalEl;
    }
}
class ModalWindow {
    constructor() {
        this.opened = false;
        this.createElement();
    }
    close() {
        document.body.removeChild(this.modalElement.element);
        this.opened = false;
        console.log("Modal closed");
        return this;
    }
    open() {
        if (typeof this.message == 'undefined')
            return this;
        this.modalElement.element = this.modalElement.getElement();
        document.body.appendChild(this.modalElement.element);
        this.opened = true;
        console.log("Modal opened", this.message);
        return this;
    }
    setMessage(message) {
        this.message = message;
        this.modalElement.setMessage(message);
        return this;
    }
    addButton(btnMessage, action) {
        var button = this.modalElement.addBtn(btnMessage), modal = this;
        button.addEventListener('click', function () {
            if (action)
                action.call(modal);
            modal.close();
        });
        return this;
    }
    createElement() {
        this.modalElement = new ModalElement();
        return true;
    }
}
//# sourceMappingURL=hy_bootstrap_modal.js.map