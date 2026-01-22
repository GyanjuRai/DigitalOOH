export type DialogFieldType = 'text'
| 'number'
| 'email'
| 'select'
| 'mutiselect'
| 'status'
| 'adsselector'
| 'datetime'
| 'file';

export interface DialogOption {
    label: string;
    value: any;
}

export interface DialogField {
    name: string;
    label: string;
    type: DialogFieldType;
    placeholder?: string;
    required?: boolean;
    value?: any;
    options?: DialogOption[];
}

export interface DialogData {
    title: string;
    fields: DialogField[];
    data?: any;
    submitLabel?: string;
    cancelLabel?: string;
}
