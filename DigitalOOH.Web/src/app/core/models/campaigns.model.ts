import { adPlaylistItem } from "./ads.model";
import { screenId } from "./screens.model";

export interface campaignsModel {
    id: string;
    name: string;
    startTime: Date;
    endTime: Date;
    screens: string;
    ads: string;
    createdAt: Date;
}

export interface campaignCreateParam {
    name: string;
    startTime: Date;
    endTime: Date;
    screens: screenId[];
    ads: adPlaylistItem[];
}