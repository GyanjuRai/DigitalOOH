import { Injectable } from "@angular/core";
import { WebApiService } from "./web-api.service";
import { Observable } from "rxjs";
import { gridResponse, responseModel } from "../models/base.model";
import { screenEditParam, screenModel, screenNameAndId, screenParam } from "../models/screens.model";

@Injectable({
    providedIn: 'root'
})
export class ScreenService {

    constructor(private api: WebApiService) {}

    getScreens() : Observable<responseModel<gridResponse<screenModel>>> {
        return this.api.get('Screens/GetScreens');
    }

    getScreensForDropdown() : Observable<responseModel<screenNameAndId[]>> {
        return this.api.get('Screens/GetScreensForDropdown');
    }

    addScreen(param : screenParam) : Observable<responseModel<screenModel>> {
        return this.api.post('Screens/ScreenAdd', param);
    }

    editScreen(param: screenEditParam) : Observable<responseModel<screenModel>> {
        return this.api.put('Screens/ScreenEdit', param);
    }
}