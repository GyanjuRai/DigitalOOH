import { DialogField, DialogOption } from "../../shared/components/dialog-box/dailog-box.model";

const mediaTypeOptions : DialogOption[] = [
    {
        label: 'Image',
        value: 1
    },
    {
        label: 'Video',
        value: 2
    }
];

export const adsField: DialogField[] = [
    {
        name: 'title',
        label: 'Ad Title',
        type:'text',
        placeholder: 'Enter ad title',
        required: true
    },
    {
        name: 'mediaType',
        label: 'Media type',
        type: 'select',
        options: mediaTypeOptions,
        required: true
    },
    {
        name: 'durationSeconds',
        label: 'Duration',
        type: 'number',
        placeholder: 'Enter duration in seconds',
        required: true
    },
    {
        name: 'file',
        label: 'Meida Upload',
        type: 'file',
        required: true
    }
];