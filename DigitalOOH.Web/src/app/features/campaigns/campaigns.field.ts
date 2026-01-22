import { DialogField } from "../../shared/components/dialog-box/dailog-box.model";

export const campaignFeild: DialogField[] = [
    {
        name: 'name',
        label: 'Campaign name',
        type: 'text',
        required: true,
        placeholder: 'Enter campaign name'
    },
    {
        name: 'startTime',
        label: 'Start time',
        type: 'datetime'
    },
    {
        name: 'endTime',
        label: 'End time',
        type: 'datetime'
    },
    {
        name: 'screens',
        label: 'Screens',
        type: 'mutiselect'
    },
    {
        name: 'ads',
        label: 'Ads',
        type: 'adsselector'
    }
]