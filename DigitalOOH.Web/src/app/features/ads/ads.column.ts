import { gridColumn } from "../../shared/components/grid-config/grid.model";

export const adsColumn : gridColumn[] = [
    {
        name: 'title',
        display: 'Title',
        type: 'text',
    },
    {
        name: 'mediaType',
        display: 'Media Type',
        type: 'media-type',
    },
    {
        name: 'mediaUrl',
        display: 'Media',
        type: 'media'
    },
    {
        name: 'durationSeconds',
        display: 'Duration',
        type: 'number'
    },
    {
        name: 'createdAt',
        display: 'Created At',
        type: 'text'
    }
]