import { gridColumn } from "../../shared/components/grid-config/grid.model";

export const screenColumn : gridColumn[] = [
    {
        name: "name",
        display: "Screen name",
        type: "text"
    },
    {
        name: "location",
        display: "Location",
        type: "text"
    },
    {
        name: "resolution",
        display: "Resolution",
        type: 'text'
    },
    {
        name: "isActive",
        display: "Status",
        type: "boolean"
    },
    {
        name: "createdAt",
        display: "Created At",
        type: 'text'
    },
    {
        name: "updatedAt",
        display: "Updated At",
        type: 'text'
    }
]