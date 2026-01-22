export interface screenModel {
    id: string;
    name: string;
    location: string;
    resolution: string;
    isActive: boolean;
    cratedAt: Date;
}

export interface screenId {
    id: string;
}

export interface screenNameAndId {
    id: string;
    name: string;
}

export interface screenParam {
    name: string;
    location: string;
    resolution: string;
    isActive: boolean;
}

export interface screenEditParam {
    id: string;
    name: string;
    location: string;
    resolution: string;
    isActive: boolean;
}