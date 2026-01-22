import { mediaTypeEnum } from "./enum";

export interface adsModel {
    id: string;
    title: string;
    mediaType: mediaTypeEnum;
    mediaUrl?: string;
    durationSeconds: number;
    createdAt: Date
}

export interface adCreateParam {
    title: string;
    mediaType: mediaTypeEnum;
    durationSeconds: number;
    file: File;
}

export interface adsNameAndId {
    id: string;
    title: string;
}

export interface adsIdParam {
    id: string;
}

export interface adPlaylistItem {
    id: string;
    playOrder: number;
}
