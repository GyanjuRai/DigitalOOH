export interface gridConfig {
    columns?: gridColumn[];
    dataSource: {
        data: any[],
        totalRows: number;
    },
    loading: boolean;
}

export interface gridColumn {
    name: string; // column name
    display?: string // column display name
    type: 'text' | 'boolean' | 'media' | 'media-type' | 'number', 
    width?: number // column width
}