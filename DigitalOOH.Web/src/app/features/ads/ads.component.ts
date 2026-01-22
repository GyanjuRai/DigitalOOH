import { Component, OnDestroy, OnInit } from "@angular/core";
import { gridConfig } from "../../shared/components/grid-config/grid.model";
import { adsColumn } from "./ads.column";
import { Subject, take, takeUntil } from "rxjs";
import { AdsService } from "../../core/services/ads.service";
import { SnackbarService } from "../../core/services/snackbar.service";
import { gridResponse, responseModel } from "../../core/models/base.model";
import { adCreateParam, adsModel } from "../../core/models/ads.model";
import { responseEnum } from "../../core/models/enum";
import { DialogData } from "../../shared/components/dialog-box/dailog-box.model";
import { adsField } from "./ads.field";
import { MatDialog } from "@angular/material/dialog";
import { DialogBoxComponent } from "../../shared/components/dialog-box/dialog-box.component";
import { ConfirmationData, ConfirmationDialogComponent } from "../../shared/components/confirmation-dialog/confirmation-dialog.component";

@Component({
    selector: 'ads',
    templateUrl: './ads.component.html',
    styleUrls: ['./ads.component.css', '../../app.component.css']
})
export class AdsComponent implements OnInit, OnDestroy {
  gridConfig: gridConfig = {
    columns: adsColumn,
    dataSource: {
      data: [],
      totalRows: 0
    },
    loading: false
  };

  private __unSubscribeAll: Subject<any>;
  selectedAd: any;

  constructor(
    private dailog: MatDialog,
    private adService: AdsService,
    private snackbarService: SnackbarService
  )
  {
    this.__unSubscribeAll = new Subject<any>();
  }

  ngOnInit(): void {
    this.loadAds();
  }

  loadAds() {
    this.adService.getAds()
    .pipe(takeUntil(this.__unSubscribeAll))
    .subscribe((response: responseModel<gridResponse<adsModel>>) => {
      if(response.type === responseEnum.sucess && response.data) {
        this.gridConfig.dataSource.data = response.data.data;
        this.gridConfig.dataSource.totalRows = response.data.totalRows;

        this.gridConfig = {...this.gridConfig} // refresh the grid
      }
    })
  }

  onRowSelect(row: any) {
    this.selectedAd = row;
  }

  onAdd() {
    const dialogData: DialogData = {
      title: 'Add ad',
      fields: adsField,
      submitLabel: 'Add',
      cancelLabel: 'Cancel'
    };

    const dialogRef = this.dailog.open(DialogBoxComponent, {
      width: '500px',
      height: 'auto',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {

        const ad = new FormData();

        ad.append('title', result.title);
        ad.append('mediaType', result.mediaType);
        ad.append('durationSeconds', result.durationSeconds);
        ad.append('file', result.file);

        this.adService.addAd(ad)
        .pipe(takeUntil(this.__unSubscribeAll))
        .subscribe((response: responseModel<adsModel>) => {
          if(response.type === responseEnum.sucess && response.data) {

            this.gridConfig.dataSource.data.push(response.data);
            this.gridConfig.dataSource.totalRows++;

            this.gridConfig = {...this.gridConfig}; //refresh grid
            this.snackbarService.success('Ad added sucessfully');
          } else {
            console.log(response);
            this.snackbarService.error('Failed to add ad');
          }
        });
      }
    });

  }

  
  onDelete() {

    if(!this.selectedAd.id) {
      this.snackbarService.warning('Please select ad frist');
      return;
    }

    const confirmData: ConfirmationData = {
      title: `Delete ${this.selectedAd?.title}`,
      message: `Are you sure ? Action cannot be undone`,
      confirmLabel: 'Delete',
      cancelLabel: 'Cancel',
      confirmColor: 'danger'
    }

    const dialogRef = this.dailog.open(ConfirmationDialogComponent, {
      width: 'auto',
      data: confirmData,
      autoFocus: true,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {

        const adId = this.selectedAd?.id;

        this.adService.removeAd(adId)
        .pipe(takeUntil(this.__unSubscribeAll))
        .subscribe((response : responseModel<boolean>) => {
          if(response.type === responseEnum.sucess && response.data) {

            const index = this.gridConfig.dataSource.data.findIndex(x => x.id === this.selectedAd.id);
            this.gridConfig.dataSource.data.splice(index, 1);
            this.gridConfig.dataSource.totalRows--;

            this.gridConfig = {...this.gridConfig} // refresh grid
            this.snackbarService.info(response.message);
          } else {
            this.snackbarService.warning(response.message);
          }
        });
      }
    });
  }

  refresh() {
    this.loadAds();
    this.selectedAd = {};
    this.snackbarService.success('Screen refreshed');
  }

  ngOnDestroy(): void {
    this.__unSubscribeAll.next(null);
    this.__unSubscribeAll.complete();
  }
}
