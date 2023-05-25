import { Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import React, { useEffect } from 'react'
import { Form } from 'react-router-dom'
import { Header, Divider, Container, Button, DropdownItemProps, DropdownItem } from 'semantic-ui-react'
import MyDropdown from '../../../app/common/forms/MyDropdown'
import MyTextInput from '../../../app/common/forms/MyTextInput'
import { useStore } from '../../../app/stores/store'
import * as Yup from 'yup';
import {v4 as uuid} from 'uuid';
import { FutureIncomeFormValues } from '../../../app/models/spendingPlan/futureIncome'

interface Props {
  date: Date
}

function FutureIncomeAdd({date}: Props) {
  const {modalStore, spendingPlanStore, extrasStore} = useStore()
  const {createIncome} = spendingPlanStore
  const {loadCheckingAccounts, checkingAccounts, loadingAcc} = extrasStore

  const validationSchema = Yup.object({
    accountName: Yup.string().required('Account name is required'),
    amount: Yup.number().positive('Amount must be positive'),
    category: Yup.string().required('Category is required')
  })

  useEffect(() => {
    if(checkingAccounts.length < 1) loadCheckingAccounts()
  }, [loadCheckingAccounts, checkingAccounts.length])

  function handleFormSubmit(income: FutureIncomeFormValues) {
    income.id = uuid()
    createIncome(income).then(modalStore.closeModal)
  }

  const dropdownItems = checkingAccounts.map((acc) => {
    return {
      key: acc.id,
      value: acc.name,
      text: acc.name
    }
  })

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
          <Header as={'h1'} content='New Future Income' textAlign='center' />
          <Divider />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyTextInput placeholder='Category' name='category' />
          <MyDropdown placeholder='Account' name='accountName' options={dropdownItems} />
          <Container
          style={{ display: 'flex', justifyContent: 'center', width: '60%' }}>
            <Button loading={isSubmitting || loadingAcc} content='Create' positive type='submit' circular />
          </Container>
        </Form>
      )}
    </Formik>
  )
}

export default observer(FutureIncomeAdd)