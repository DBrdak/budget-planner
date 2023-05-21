import { FieldInputProps, useField } from 'formik'
import React, { useState } from 'react'
import { Dropdown, DropdownItemProps, DropdownProps, Form, Label } from 'semantic-ui-react'

interface Props {
  placeholder: string,
  name: string,
  options: DropdownItemProps[],
  label?: string,
  type?: string,
}

function MyDropdown(props: Props) {
  const [field, meta, helpers] = useField(props.name);

  const handleChange = (event: React.SyntheticEvent<HTMLElement>, data: DropdownProps) => {
    const { name, value } = data;
    const newField: FieldInputProps<any> = {
      name: props.name,
      value,
      onBlur: field.onBlur,
      onChange: field.onChange,
    };
    helpers.setValue(value);
    helpers.setTouched(true);
  }

  return (
    <Form.Field error={meta.touched && !!meta.error}>
      {props.label &&
        <Label>{props.label}</Label>}
      <Dropdown selection
        options={props.options}
        value={field.value || ''}
        onChange={handleChange}
        placeholder={props.placeholder} />
      {meta.touched && meta.error ? (
        <Label basic color='red'>{meta.error}</Label>
      ) : null}
    </Form.Field>
  )
}

export default MyDropdown