export interface IModalElement {
    element: HTMLDivElement;
    header: HTMLDivElement;
    body: HTMLDivElement;
    footer: HTMLDivElement;
    buttons: HTMLButtonElement[];
    addBtn(string): HTMLButtonElement;
    setMessage(string): void;
    getElement(): HTMLDivElement;
}
export interface IModalWindow {
    opened: boolean;
    message: string;
    modalElement: IModalElement;
    open(): IModalWindow;
    close(): IModalWindow;
    setMessage(string): IModalWindow;
    addButton(string, Function): IModalWindow;
    createElement(): boolean;
}