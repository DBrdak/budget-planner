import { Formik, Form, ErrorMessage } from 'formik'
import React from 'react'
import { Navigate, useNavigate } from 'react-router-dom'
import { Button, Container, DropdownItemProps, Header, Label } from 'semantic-ui-react';
import MyTextInput from '../../../app/common/forms/MyTextInput';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../app/stores/store';
import MyDropdown from '../../../app/common/forms/MyDropdown';

function IncomeForm() {
  const {modalStore} = useStore()

  // To będzie do wywalenia ->
  const navigate = useNavigate();

  const submit = (values: any) => {
    modalStore.closeModal()
    console.log(values)
    navigate('/main', { replace: true });
  };

  const categories: DropdownItemProps[] = [
    { text: 'Option 1', value: 'option1' },
    { text: 'Option 2', value: 'option2' },
  ]
  // <-

  return (
    <Formik
    initialValues={{title: '', amount: '', 
    category: '', account: '', date: ''}}
    onSubmit={(values, {setErrors}) => submit(values)}>
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header content='New Income' textAlign='center' />
          <MyTextInput placeholder='Title' name='title' />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyDropdown placeholder='Category' name='category' options={categories} />
          <MyDropdown placeholder='Account' name='account' options={categories} />
          <MyTextInput placeholder='Date' name='date' />
          <Container
          style={{ display: 'flex', justifyContent: 'space-between', width: '60%' }}>
            <Button loading={isSubmitting} icon='check' positive type='submit' circular />
            <Button loading={isSubmitting} icon='close' negative type='submit' circular />
          </Container>
        </Form>
      )}
    </Formik>
  )
}

export default observer(IncomeForm)