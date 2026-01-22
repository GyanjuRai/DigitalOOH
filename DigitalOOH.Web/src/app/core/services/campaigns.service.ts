import { Injectable } from "@angular/core";
import { WebApiService } from "./web-api.service";
import { Observable } from "rxjs";
import { gridResponse, responseModel } from "../models/base.model";
import { campaignCreateParam, campaignsModel } from "../models/campaigns.model";

@Injectable({
    providedIn: 'root'
})
export class CampaignsService {

    constructor(
        private api: WebApiService
    )
    {}

    getCampaigns(): Observable<responseModel<gridResponse<campaignsModel>>> {
        return this.api.get('Campaigns/GetCampaigns');
    } 

    addCampaign(param: campaignCreateParam): Observable<responseModel<campaignsModel>> {
        return this.api.post('Campaigns/AddCampaign', param);
    }
}