import { DialogField, DialogOption } from "../../shared/components/dialog-box/dailog-box.model";


const statusOptions: DialogOption[] = [
    {
        label: "Active",
        value: true
    },
    {
        label: "Inactive",
        value: false
    }
]

export const screenDialogboxField: DialogField[] = [
    {
        name: "name",
        label: "Screen name",
        type: "text",
        required: true,
        placeholder: "Enter screen name"
    },
    {
        name: "location",
        label: "Location",
        type: "text",
        required: true,
        placeholder: "Enter location"
    },
    {
        name: "resolution",
        label: "Resolution (eg. 1080x1080)",
        type: "text",
        required:  true,
        placeholder: "Enter resolution"
    },
    {
        name: "isActive",
        label: "Status",
        type: 'status',
        required: true,
        options: statusOptions
    }
]

