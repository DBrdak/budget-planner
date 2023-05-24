import { Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import React from 'react'
import { Form } from 'react-router-dom'
import { Header, Divider, Container, Button, DropdownItemProps } from 'semantic-ui-react'
import MyDropdown from '../../../app/common/forms/MyDropdown'
import MyTextInput from '../../../app/common/forms/MyTextInput'
import { useStore } from '../../../app/stores/store'

interface Props {
  header: string,
  date: Date
}

function IncomeAdd({header, date}: Props) {
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
    initialValues={{title: '', amount: '', 
    category: '', account: '', date: ''}}
    onSubmit={(values, {setErrors}) => submit(values)} >
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as={'h1'} content={header} textAlign='center' />
          <Divider />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyTextInput placeholder='Category' name='category' />
          <MyDropdown placeholder='Account' name='account' options={categories} />
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

export default observer(IncomeAdd)