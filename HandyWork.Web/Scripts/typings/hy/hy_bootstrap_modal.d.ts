interface bs_modal {
    //Modal.VERSION  = '3.3.7'
    $backdrop: JQuery,
    $body: JQuery,
    $dialog: JQuery,
    $element: JQuery,
    bodyIsOverflowing: boolean
    ignoreBackdropClick: boolean
    isShown: boolean
    options: Object
    originalBodyPad: string
    scrollbarWidth: number,

    toggle(_relatedTarget: any): void;
    show(_relatedTarget: any): void;
    hide(e: Event): void;
}
interface IModalElement {
    modal: HTMLDivElement;
    modal_diag: HTMLDivElement;
    modal_content: HTMLDivElement;
    header: HTMLDivElement;
    body: HTMLDivElement;
    footer: HTMLDivElement;
    buttons: HTMLButtonElement[];
    addBtn(string): HTMLButtonElement;
    setHeaderHtml(innerHtml:string): void;
    setBodyHtml(innerHtml: string): void;
}
export interface Ihy_modal {
    readonly isShown: boolean;
    setHeaderHtml(innerHtml: string): Ihy_modal;
    setBodyHtml(innerHtml: string): Ihy_modal;
    addButton(string, Function): Ihy_modal;

    show(relatedTarget?: any): void;
    hide(relatedTarget?: any): void;
    /**
     *show 方法调用之后立即触发该事件。返回false,则阻止关闭模态框。
     */
    show_bs_modal?(relatedTarget?: any): boolean;
    /**
     *此事件在模态框已经显示出来（并且同时在 CSS 过渡效果完成）之后被触发。
     */
    shown_bs_modal?(relatedTarget?: any): void;
    /**
     *hide 方法调用之后立即触发该事件。返回false,则阻止打开模态框
     */
    hide_bs_modal?(relatedTarget?: any): boolean;
    /**
     *此事件在模态框被隐藏（并且同时在 CSS 过渡效果完成）之后被触发。
     */
    hidden_bs_modal?(relatedTarget?: any): void;
}