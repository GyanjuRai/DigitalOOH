import { 
    Component, 
    EventEmitter, 
    Input, 
    OnChanges, 
    Output, 
    SimpleChanges
} from "@angular/core";

import { gridConfig } from "./grid.model";
import { mediaTypeEnum } from "../../../core/models/enum";
import { AppConst } from "../../../app.const";

@Component({
    selector: 'grid-config',
    templateUrl: './grid-config.component.html',
    styleUrl: './grid-config.component.css'
})
export class GridComponent implements OnChanges {

    @Input() config!: gridConfig;

    @Output() rowSelect = new EventEmitter<any>();

    displayedColumns: string[] = [];
    selectedRow: any;
    mediaType = mediaTypeEnum;
    apiUrl: string = AppConst.data.apiBaseUrl ?? '';

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['config']) {
            this.displayedColumns = this.config.columns?.map(c => c.name) ?? [];
        }
    }

    getColumnType(columnName: string): string {
        return this.config.columns?.find(c => c.name === columnName)?.type ?? 'text';
    }

    getColumnDisplay(columnName: string): string {
        const column = this.config.columns?.find(c => c.name === columnName);
        return column?.display || columnName;
    }
    
    getMediaUrl(path: string): string {
        return `${this.apiUrl}${path}`
    }

    selectRow(row: any): void {
        this.selectedRow = row;
        this.rowSelect.emit(row);
    }
}
