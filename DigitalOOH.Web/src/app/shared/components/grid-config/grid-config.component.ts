import { 
    Component, 
    EventEmitter, 
    Input, 
    OnChanges, 
    Output, 
    SimpleChanges
} from "@angular/core";

import { gridConfig } from "./grid.model";

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

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['config']) {
            this.displayedColumns = this.config.columns?.map(c => c.name) ?? [];
        }
    }

    getColumnDisplay(columnName: string): string {
        const column = this.config.columns?.find(c => c.name === columnName);
        return column?.display || columnName;
    }

    selectRow(row: any): void {
        this.selectedRow = row;
        this.rowSelect.emit(row);
    }
}
