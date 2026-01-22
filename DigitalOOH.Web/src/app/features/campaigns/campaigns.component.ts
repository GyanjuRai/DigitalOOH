import { Component, OnDestroy, OnInit } from "@angular/core";
import { gridConfig } from "../../shared/components/grid-config/grid.model";
import { campaignColumns } from "./campaignn.columns";
import { Subject, take, takeUntil } from "rxjs";
import { SnackbarService } from "../../core/services/snackbar.service";
import { MatDialog } from "@angular/material/dialog";
import { DialogData, DialogField, DialogOption } from "../../shared/components/dialog-box/dailog-box.model";
import { campaignFeild } from "./campaigns.field";
import { DialogBoxComponent } from "../../shared/components/dialog-box/dialog-box.component";
import { ScreenService } from "../../core/services/screens.service";
import { AdsService } from "../../core/services/ads.service";
import { gridResponse, responseModel } from "../../core/models/base.model";
import { screenNameAndId } from "../../core/models/screens.model";
import { responseEnum } from "../../core/models/enum";
import { adsNameAndId } from "../../core/models/ads.model";
import { CampaignsService } from "../../core/services/campaigns.service";
import { campaignCreateParam, campaignsModel } from "../../core/models/campaigns.model";

@Component({
    selector: 'campaigns',
    templateUrl: './campaigns.component.html',
    styleUrls: [
      './campaigns.component.css',
      '../../app.component.css'
    ]
})
export class CampaignsComponent implements OnInit, OnDestroy {
  gridConfig: gridConfig = {
    columns: campaignColumns,
    dataSource: {
      data: [],
      totalRows: 0
    },
    loading: false
  };

  private __unSubscribeAll: Subject<any>;
  selectedCampaign: any;
  screensOptions: DialogOption[] = [];
  adsOptions: DialogOption[] = [];

  constructor(
    private dialog: MatDialog,
    private snackbarService: SnackbarService,
    private screenService: ScreenService,
    private adsService: AdsService,
    private campaignService: CampaignsService
  ) 
  {
    this.__unSubscribeAll = new Subject<any>();
  }

  ngOnInit(): void {
    this.loadCampaigns();
    this.loadScreens();
    this.loadAds();
  }

  loadCampaigns() {
    this.campaignService.getCampaigns()
    .pipe(takeUntil(this.__unSubscribeAll))
    .subscribe((response: responseModel<gridResponse<campaignsModel>>) => {
      if(response.type === responseEnum.sucess && response.data) {
        this.gridConfig.dataSource = {
          data: response.data.data,
          totalRows: response.data.totalRows
        };

        this.gridConfig = {...this.gridConfig}; // refresh grid
      }
    })
  }

  loadScreens() {
    this.screenService.getScreensForDropdown()
    .pipe(takeUntil(this.__unSubscribeAll))
    .subscribe((response : responseModel<screenNameAndId[]>) => {
      if(response.type === responseEnum.sucess && response.data) {
        this.screensOptions  = response.data.map(s => ({
          label: s.name,
          value: s.id
        }));
      }
    });
  }

  loadAds() {
    this.adsService.getAdsForDropdown()
    .pipe(takeUntil(this.__unSubscribeAll))
    .subscribe((response: responseModel<adsNameAndId[]>) => {
      if(response.type === responseEnum.sucess && response.data) {
        this.adsOptions = response.data.map(a => ({
          label: a.title,
          value: a.id
        }));
      }
    });
  }

  buildCampaignFields(): DialogField[] {
    return campaignFeild.map(field => {

      if(field.name === 'screens') {
        return {
          ...field,
          options: this.screensOptions
        }
      }

      if(field.name === 'ads') {
        return {
          ...field,
          options: this.adsOptions
        }
      }

      return field;
    })
  }

  onRowSelect(row: any) {
    this.selectedCampaign = row;
    console.log(row);
  }

  onAdd() {
    
    const screenFieldData: DialogData = {
      title: 'Add Campaigns',
      fields: this.buildCampaignFields(),
      submitLabel: 'Add',
      cancelLabel: 'Cancel'
    };

    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '700px',
      data: screenFieldData,
      disableClose: true
    });

    dialogRef.afterClosed()
    .subscribe(result => {
      if(result) {
        
        const param: campaignCreateParam = {
          name: result.name,
          startTime: result.startTime,
          endTime: result.endTime,
          screens: result.screens,
          ads: result.ads
        }

        this.campaignService.addCampaign(param)
        .pipe(takeUntil(this.__unSubscribeAll))
        .subscribe((response: responseModel<campaignsModel>) => {
          if(response.type === responseEnum.sucess && response.data) {

            this.gridConfig.dataSource.data.push(response.data);
            this.gridConfig.dataSource.totalRows++;
            this.gridConfig = {...this.gridConfig}; // refresh grid

            this.snackbarService.success('Campaign added successfully');
          }
        });
      }
    });
  }

  refresh() {
    this.selectedCampaign = {};
    this.loadCampaigns();
  }

  ngOnDestroy(): void {
    this.__unSubscribeAll.next(null);
    this.__unSubscribeAll.complete();
  }
}
