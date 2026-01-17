import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

export interface DialogData {
  title: string;
  fields: DialogField[];
  data?: any; // For edit mode - pre-fill data
  submitLabel?: string;
  cancelLabel?: string;
}

export interface DialogField {
  name: string;
  label: string;
  type: string; // 'text', 'number', 'email', etc.
  required?: boolean;
  placeholder?: string;
  value?: any;
}

@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrl: './dialog-box.component.css'
})
export class DialogBoxComponent implements OnInit {
  form: FormGroup;
  title: string;
  fields: DialogField[];
  submitLabel: string;
  cancelLabel: string;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<DialogBoxComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData: DialogData
  ) {
    this.title = dialogData.title;
    this.fields = dialogData.fields;
    this.submitLabel = dialogData.submitLabel || 'Save';
    this.cancelLabel = dialogData.cancelLabel || 'Cancel';
    this.form = this.fb.group({});
  }

  ngOnInit(): void {
    // Build form based on fields
    this.fields.forEach(field => {
      const validators = field.required ? [Validators.required] : [];
      const value = this.dialogData.data?.[field.name] || field.value || '';
      this.form.addControl(field.name, this.fb.control(value, validators));
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  getFieldControl(fieldName: string) {
    return this.form.get(fieldName);
  }
}
