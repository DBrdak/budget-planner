import { Formik } from 'formik';
import { observer } from 'mobx-react-lite'
import React from 'react'
import { useNavigate, Form } from 'react-router-dom';
import { DropdownItemProps, Header, Divider, Container, Button } from 'semantic-ui-react';
import MyDateInput from '../../../app/common/forms/MyDateInput';
import MyDropdown from '../../../app/common/forms/MyDropdown';
import MyTextInput from '../../../app/common/forms/MyTextInput';
import { useStore } from '../../../app/stores/store';

interface Props {
  date: Date
}

function FutureSavingAdd({date}: Props) {
  const {modalStore} = useStore()

  // To będzie do wywalenia ->

  const submit = (values: any) => {
    modalStore.closeModal()
  };

  const categories: DropdownItemProps[] = [
    { text: 'Option 1', value: 'option1' },
    { text: 'Option 2', value: 'option2' },
  ]
  // <-
  return (
    <Formik
    initialValues={{goal: '', amount: '', 
    fromAccount: '', toAccount: '', date: ''}}
    onSubmit={(values, {setErrors}) => submit(values)}>
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as={'h1'} content='New Future Saving' textAlign='center' />
          <Divider />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyDropdown placeholder='Goal' name='goal' options={categories}  />
          <MyDropdown placeholder='From Account' name='fromAccount' options={categories} />
          <MyDropdown placeholder='To Account' name='toAccount' options={categories} />
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

export default observer(FutureSavingAdd)