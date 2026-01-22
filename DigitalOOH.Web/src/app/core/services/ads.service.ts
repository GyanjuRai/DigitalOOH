import { Injectable } from "@angular/core";
import { WebApiService } from "./web-api.service";
import { Observable } from "rxjs";
import { gridResponse, responseModel } from "../models/base.model";
import { adCreateParam, adsIdParam, adsModel, adsNameAndId } from "../models/ads.model";

@Injectable({
    providedIn: 'root'
})
export class AdsService {
    constructor(
       private api: WebApiService
    ) {}

    getAds(): Observable<responseModel<gridResponse<adsModel>>> {
        return this.api.get('AdsMng/GetAds');
    }

    getAdsForDropdown(): Observable<responseModel<adsNameAndId[]>> {
        return this.api.get('AdsMng/GetAdsForDropdown');
    }

    addAd(param: any): Observable<responseModel<adsModel>> {
        return this.api.post('AdsMng/AdAdd', param);
    }

    removeAd(param: adsIdParam): Observable<responseModel<boolean>> {
        return this.api.delete('AdsMng/AdRemove', param);
    }
}