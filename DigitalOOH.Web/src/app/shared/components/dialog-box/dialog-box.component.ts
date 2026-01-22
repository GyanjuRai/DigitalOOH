import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DialogData, DialogField } from './dailog-box.model';

@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrl: './dialog-box.component.css'
})
export class DialogBoxComponent implements OnInit {
  formGroup: FormGroup;

  title: string;
  fields: DialogField[];
  submitLabel: string;
  cancelLabel: string;
  // adsField?: DialogField;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<DialogBoxComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData: DialogData
  ) 
  {
    this.title = this.dialogData.title;
    this.fields = this.dialogData.fields;
    this.submitLabel = this.dialogData.submitLabel ?? 'Save';
    this.cancelLabel = this.dialogData.cancelLabel ?? 'Cancel';
    this.formGroup = this.fb.group({});
  }

  ngOnInit(): void {

    this.fields.forEach(field => {
      if (field.type === 'adsselector') {
        this.formGroup.addControl(field.name, this.fb.array([]));
        return;
      }

      if (field.type === 'file') {
        this.formGroup.addControl(
          field.name,
          this.fb.control(null, field.required ? Validators.required : [])
        );
        return;
      }

      const validators = field.required ? [Validators.required] : [];
      const value = this.dialogData.data?.[field.name] ?? field.value ?? null;
      this.formGroup.addControl(field.name, this.fb.control(value, validators));
    });
  }

  /* ===== ADS HELPERS ===== */

  get adsArray(): FormArray {
    return this.formGroup.get('ads') as FormArray;
  }

  addAd(ad: { value: string; label: string }) {
    if (this.isAdSelected(ad.value)) return;

    this.adsArray.push(
      this.fb.group({
        id: [ad.value, Validators.required],
        adName: [ad.label],
        playOrder: [this.adsArray.length + 1, Validators.min(1)]
      })
    );
  }

  removeAd(index: number) {
    this.adsArray.removeAt(index);
  }

  isAdSelected(adId: string): boolean {
    return this.adsArray.controls.some(c => c.value.id === adId);
  }

  compareAd = (a: any, b: any) => a?.value === b?.value;

  /* ===== FILE ===== */

  onFileSelected(event: Event, fieldName: string) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      this.formGroup.get(fieldName)?.setValue(file);
    }
  }

  /* ===== SUBMIT ===== */

  onSubmit(): void {
    if (!this.formGroup.valid) return;

    const value = { ...this.formGroup.value };

    this.fields
      .filter(f => f.type === 'datetime')
      .forEach(f => {
        if (value[f.name]) {
          value[f.name] = new Date(value[f.name]).toISOString();
        }
      });

    const hasAdsSelector = this.fields.some(f => f.type === 'adsselector');

    if (hasAdsSelector && Array.isArray(value.ads)) {
      value.ads = value.ads.map((s: any) => ({
        id: s.id,
        playOrder: s.playOrder
      }));
    }

    this.dialogRef.close(value);
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
