import { Injectable } from "@angular/core";
import { environment } from "../env/environment";

@Injectable({
    providedIn: 'root'
})

export class AppConst {
    static data: any;

    constructor() {}

    async loadConfig(): Promise<any> {
        const filePath = `/assets/${environment.production ? environment.prodConfig : environment.devConfig}?t=${new Date().getTime()}`;
        const response = await fetch(filePath);

        if(response.ok){
            try{
                AppConst.data = await response.json();
            }
            catch (error: any) {
                console.error(`Could not find file path ${environment.production ? '' : error.message}.`);
            }

            if(AppConst.data && !Object.keys((AppConst.data)).length) {
                console.error('No data found in file.');
            }
        } else{
            console.log('Failed to load file.');
        }
    }
}