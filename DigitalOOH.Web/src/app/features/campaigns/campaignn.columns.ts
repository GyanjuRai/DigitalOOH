import { gridColumn } from "../../shared/components/grid-config/grid.model";

export const campaignColumns: gridColumn[] = [
    {
        name: 'name',
        display: 'Campaign Name',
        type: 'text'
    },
    {
        name: 'startTime',
        display: 'Start Time',
        type: 'text'
    },
    {
        name: 'endTime',
        display: 'End Time',
        type: 'text'
    },
    {
        name: 'screens',
        display: 'Assigned Screens',
        type: 'text'
    },
    {
        name: 'ads',
        display: 'Assigned Ads',
        type: 'text'
    },
    {
        name: 'createdAt',
        display: 'Created At',
        type: 'text'
    }
]