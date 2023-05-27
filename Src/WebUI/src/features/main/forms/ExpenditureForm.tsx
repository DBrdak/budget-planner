import { Formik, Form } from 'formik'
import { useNavigate } from 'react-router-dom'
import { Button, Container, Divider, DropdownItemProps, Header } from 'semantic-ui-react';
import MyTextInput from '../../../app/common/forms/MyTextInput';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../app/stores/store';
import MyDropdown from '../../../app/common/forms/MyDropdown';
import MyDateInput from '../../../app/common/forms/MyDateInput';
import * as Yup from 'yup';
import {v4 as uuid} from 'uuid';
import { useEffect } from 'react';
import { FutureExpenditureFormValues } from '../../../app/models/spendingPlan/futureExpenditure';
import { ExpenditureFormValues } from '../../../app/models/expenditure';
import { router } from '../../../app/router/Routes';

function ExpenditureForm() {
  const {modalStore, extrasStore, budgetStore} = useStore()
  const {loading, createExpenditure} = budgetStore
  const {loadCheckingAccounts, loadExpenditureCategories, 
    expenditureCategories, checkingAccounts, loadingAcc, loadingCat} = extrasStore

  const validationSchema = Yup.object({
    title: Yup.string().required('Title is required'),
    accountName: Yup.string().required('Account name is required'),
    amount: Yup.number().positive('Amount must be positive'),
    category: Yup.string().required('Category is required')
  })

  useEffect(() => {
    if(checkingAccounts.length === 0) loadCheckingAccounts()
    if(expenditureCategories.length === 0) loadExpenditureCategories()
  }, [loadCheckingAccounts, loadExpenditureCategories,
    checkingAccounts.length, expenditureCategories.length])

  function handleFormSubmit(expenditure: ExpenditureFormValues) {
    expenditure.id = uuid()
    createExpenditure(expenditure).then(modalStore.closeModal)
  }

  const DIcategories = expenditureCategories.map((ec) => {
    return {
      key: ec.id,
      value: ec.value,
      text: ec.value
    }
  })

  const DIaccounts = checkingAccounts.map((acc) => {
    return {
      key: acc.id,
      value: acc.name,
      text: acc.name
    }
  })


  return (
    <Formik
    validationSchema={validationSchema}
    initialValues={{title: '', amount: 0, 
    category: '', accountName: '', date: new Date()}}
    onSubmit={(values, {setErrors}) => handleFormSubmit(values)} >
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as={'h1'} content='New Expenditure' textAlign='center' />
          <Divider />
          <MyTextInput placeholder='Title' name='title' />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyDropdown placeholder='Category' name='category' options={DIcategories} />
          <MyDropdown placeholder='Account' name='accountName' options={DIaccounts} />
          <MyDateInput 
          placeholderText='Date'
          name='date' 
          dateFormat='d MMMM yyyy' />
          <Container
          style={{ display: 'flex', justifyContent: 'center', width: '60%' }}>
            <Button loading={loading || loadingAcc || loadingCat} content='Create' positive type='submit' circular />
          </Container>
        </Form>
      )}
    </Formik>
  )
}

export default observer(ExpenditureForm)