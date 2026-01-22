import { Component, OnDestroy, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { gridConfig } from "../../shared/components/grid-config/grid.model";
import { DialogBoxComponent } from "../../shared/components/dialog-box/dialog-box.component";
import { ConfirmationDialogComponent, ConfirmationData } from "../../shared/components/confirmation-dialog/confirmation-dialog.component";
import { SnackbarService } from "../../core/services/snackbar.service";
import { screenColumn } from "./screens.columns";
import { DialogData } from "../../shared/components/dialog-box/dailog-box.model";
import { screenDialogboxField } from "./screens.field";
import { ScreenService } from "../../core/services/screens.service";
import { Subject, take, takeUntil } from "rxjs";
import { screenEditParam, screenModel, screenParam } from "../../core/models/screens.model";
import { gridResponse, responseModel } from "../../core/models/base.model";
import { responseEnum } from "../../core/models/enum";

@Component({
    selector: 'screens',
    templateUrl: './screens.component.html',
    styleUrls: [
      './screens.component.css',
      '../../app.component.css'
    ]
})
export class ScreensComponent implements OnInit, OnDestroy {

  gridConfig: gridConfig = {
    columns: screenColumn,
    dataSource: {
    data: [],
      totalRows: 0
    },
    loading: false
  };

  private __unSubscribeAll: Subject<any>;
  selectedScreen: any;

  constructor(
    private dialog: MatDialog,
    private snackbarService: SnackbarService,
    private screenSerivce: ScreenService,
  ) 
  {
    this.__unSubscribeAll = new Subject<any>();
  }

  ngOnInit(): void {
    this.loadScreens();
  }

  loadScreens() {
    this.screenSerivce.getScreens()
    .pipe(takeUntil(this.__unSubscribeAll))
    .subscribe((response: responseModel<gridResponse<screenModel>>) => {
      if(response.type === responseEnum.sucess && response.data) {

        this.gridConfig.dataSource.data = response.data.data;
        this.gridConfig.dataSource.totalRows = response.data.totalRows;

        this.gridConfig = {...this.gridConfig} //refresh the grid
      }
    })
  }

  onRowSelect(row: any) {
    this.selectedScreen = row;
  }

  onAdd() {
    const dialogData: DialogData = {
      title: 'Add Screen',
      fields: screenDialogboxField,
      submitLabel: 'Add',
      cancelLabel: 'Cancel'
    };
    

    const dialogRef = this.dialog.open(DialogBoxComponent, {
      autoFocus: true,
      width: '500px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const param : screenParam = {...result};

        this.screenSerivce.addScreen(param)
        .pipe(takeUntil(this.__unSubscribeAll))
        .subscribe((response: responseModel<screenModel>) => {
          if(response.type === responseEnum.sucess && response.data) {

            this.gridConfig.dataSource.data.push(response.data);
            this.gridConfig.dataSource.totalRows++;
            this.snackbarService.success('Screen added successfully');
            this.gridConfig = {...this.gridConfig} //refresh the grid
          } else {
            this.snackbarService.error('Screen add failed');
          }
        });
      }
    });
  }

  onEdit() {
    if (!this.selectedScreen) {
      this.snackbarService.warning('Please select a screen to edit');
      return;
    }

    const dialogData: DialogData = {
      title: 'Edit Screen',
      fields: screenDialogboxField,
      data: this.selectedScreen, // Pre-fill with selected row data
      submitLabel: 'Update',
      cancelLabel: 'Cancel'
    };

    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '500px',
      data: dialogData,
      disableClose: false
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const param : screenEditParam = result;
        param.id = this.selectedScreen.id;

        this.screenSerivce.editScreen(param)
        .pipe(takeUntil(this.__unSubscribeAll))
        .subscribe((response : responseModel<screenModel>) => {
          if(response.type === responseEnum.sucess && response.data){

            let index: number = this.gridConfig.dataSource.data.findIndex(item => item.id === response.data.id);
            this.gridConfig.dataSource.data[index] = response.data;
  
            this.gridConfig = {...this.gridConfig}; // refresh grid
            this.snackbarService.success('Screen updated sucessfully');
          } else {
            this.snackbarService.error('Screen updated failed');
          }
        })


        this.snackbarService.success('Screen updated successfully');
      }
    });
  }

  refresh() {
    this.loadScreens();
    this.selectedScreen = null;
    this.snackbarService.success('Screen refreshed');
  }

  ngOnDestroy(): void {
    this.__unSubscribeAll.next(null);
    this.__unSubscribeAll.complete();
  }
}
