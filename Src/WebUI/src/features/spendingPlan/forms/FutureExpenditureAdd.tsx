import { Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import React from 'react'
import { Form } from 'react-router-dom'
import { Header, Divider, Container, Button, DropdownItemProps } from 'semantic-ui-react'
import MyDropdown from '../../../app/common/forms/MyDropdown'
import MyTextInput from '../../../app/common/forms/MyTextInput'
import { useStore } from '../../../app/stores/store'
import * as Yup from 'yup';
import {v4 as uuid} from 'uuid';
import { FutureExpenditureFormValues } from '../../../app/models/spendingPlan/futureExpenditure'

interface Props {
  date: Date
}

function FutureExpenditureAdd({date}: Props) {
  const {modalStore, spendingPlanStore} = useStore()
  const {createExpenditure} = spendingPlanStore

  const validationSchema = Yup.object({
    accountName: Yup.string().required('Account name is required'),
    amount: Yup.number().positive('Amount must be positive'),
    category: Yup.string().required('Category is required')
  })

  function handleFormSubmit(expenditure: FutureExpenditureFormValues) {
    expenditure.id = uuid()
    createExpenditure(expenditure).then(modalStore.closeModal)
  }

  const acc:DropdownItemProps[] = [{ key: '1', text: 'Checking account 02', value: 'Checking account 01' }]
  //TODO
  //*Fix the dropdown error display
  //*Add account list to dropdown
  //*Manage other props
  return (
    <Formik
    validationSchema={validationSchema}
    enableReinitialize
    initialValues={{amount: 0, 
    category: '', accountName: '', date: date}}
    onSubmit={(values) => handleFormSubmit(values)} >
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as={'h1'} content='New Future Expenditure' textAlign='center' />
          <Divider />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyTextInput placeholder='Category' name='category' />
          <MyDropdown placeholder='Account' name='accountName' options={acc} />
          <Container
          style={{ display: 'flex', justifyContent: 'center', width: '60%' }}>
            <Button loading={isSubmitting} content='Create' positive type='submit' circular onClick={() => handleSubmit} />
          </Container>
        </Form>
      )}
    </Formik>
  )
}

export default observer(FutureExpenditureAdd)