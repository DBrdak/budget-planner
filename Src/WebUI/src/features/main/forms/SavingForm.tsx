import { Formik, Form } from 'formik'
import { useNavigate } from 'react-router-dom'
import { Button, Container, Divider, DropdownItemProps, Header } from 'semantic-ui-react';
import MyTextInput from '../../../app/common/forms/MyTextInput';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../app/stores/store';
import MyDropdown from '../../../app/common/forms/MyDropdown';
import MyDateInput from '../../../app/common/forms/MyDateInput';
import {v4 as uuid} from 'uuid';
import * as Yup from 'yup';
import { useEffect } from 'react';
import {SavingFormValues} from '../../../app/models/saving'
import { router } from '../../../app/router/Routes';

function SavingForm() {
  const {modalStore, extrasStore, budgetStore} = useStore()
  const {loading, createSaving} = budgetStore
  const {loadCheckingAccounts, loadSavingAccounts, loadGoalNames, goals,
    checkingAccounts, savingAccounts, loadingAcc, loadingGoal} = extrasStore

  const validationSchema = Yup.object({
    fromAccountName: Yup.string().required('Source account name is required'),
    toAccountName: Yup.string().required('Destination account name is required'),
    amount: Yup.number().positive('Amount must be positive'),
    date: Yup.date().required('Date is required')
  })

  useEffect(() => {
    if(checkingAccounts.length < 1) {
      loadCheckingAccounts()
    }
    if(savingAccounts.length < 1){
        loadSavingAccounts()
      }
    if(goals.length < 1) {
      loadGoalNames()
    }
  }, [loadCheckingAccounts, loadSavingAccounts, loadGoalNames,
    savingAccounts.length, checkingAccounts.length, goals.length])

  function handleFormSubmit(saving: SavingFormValues) {
    saving.id = uuid()
    createSaving(saving).then(modalStore.closeModal)
  }

  const DIcheckingAccounts = checkingAccounts.map((acc) => {
    return {
      key: acc.id,
      value: acc.name,
      text: acc.name
    }
  })

  const DIsavingAccounts = savingAccounts.map((acc) => {
    return {
      key: acc.id,
      value: acc.name,
      text: acc.name
    }
  })

  const DIgoals = goals.map((g) => {
    return {
      key: g.id,
      value: g.name,
      text: g.name
    }
  })

  return (
    <Formik
    validationSchema={validationSchema}
    initialValues={{goalName: '', amount: 0, 
    fromAccountName: '', toAccountName: '', date: new Date()}}
    onSubmit={(values, {setErrors}) => handleFormSubmit(values)}>
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as={'h1'} content='New Saving' textAlign='center' />
          <Divider />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyDropdown placeholder='Goal' name='goalName' options={DIgoals}  />
          <MyDropdown placeholder='From Account' name='fromAccountName' options={DIcheckingAccounts} />
          <MyDropdown placeholder='To Account' name='toAccountName' options={DIsavingAccounts} />
          <MyDateInput 
          placeholderText='Date'
          name='date' 
          dateFormat='d MMMM, yyyy' />
          <Container
          style={{ display: 'flex', justifyContent: 'center', width: '60%' }}>
            <Button loading={isSubmitting || loadingAcc || loadingGoal || loading} content='Create' positive type='submit' circular />
          </Container>
        </Form>
      )}
    </Formik>
  )
}

export default observer(SavingForm)