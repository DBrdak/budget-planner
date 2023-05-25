import { Formik } from 'formik';
import { observer } from 'mobx-react-lite'
import React, { useEffect } from 'react'
import { useNavigate, Form } from 'react-router-dom';
import { DropdownItemProps, Header, Divider, Container, Button } from 'semantic-ui-react';
import MyDateInput from '../../../app/common/forms/MyDateInput';
import MyDropdown from '../../../app/common/forms/MyDropdown';
import MyTextInput from '../../../app/common/forms/MyTextInput';
import { useStore } from '../../../app/stores/store';
import { FutureIncomeFormValues } from '../../../app/models/spendingPlan/futureIncome';
import {v4 as uuid} from 'uuid';
import * as Yup from 'yup';
import { FutureSavingFormValues } from '../../../app/models/spendingPlan/futureSaving';

interface Props {
  date: Date
}

function FutureSavingAdd({date}: Props) {
  const {modalStore, spendingPlanStore, extrasStore} = useStore()
  const {createSaving} = spendingPlanStore
  const {loadCheckingAccounts, loadSavingAccounts, checkingAccounts, savingAccounts, loadingAcc} = extrasStore

  const validationSchema = Yup.object({
    fromAccountName: Yup.string().required('Source account name is required'),
    toAccountName: Yup.string().required('Destination account name is required'),
    amount: Yup.number().positive('Amount must be positive')
  })

  useEffect(() => {
    if(checkingAccounts.length < 1) {
      loadCheckingAccounts()
    }
    if(savingAccounts.length < 1){
        loadSavingAccounts()
      }
  }, [loadCheckingAccounts, loadSavingAccounts, savingAccounts.length, checkingAccounts.length])

  function handleFormSubmit(saving: FutureSavingFormValues) {
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

  return (
    <Formik
      validationSchema={validationSchema}
      enableReinitialize
      initialValues={{amount: 0, 
      goalName: '', fromAccountName: '', toAccountName: '', date: date}}
      onSubmit={(values) => handleFormSubmit(values)} >
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as={'h1'} content='New Future Saving' textAlign='center' />
          <Divider />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyDropdown placeholder='Goal' name='goalName' options={DIcheckingAccounts}  />
          <MyDropdown placeholder='From Account' name='fromAccountName' options={DIcheckingAccounts} />
          <MyDropdown placeholder='To Account' name='toAccountName' options={DIsavingAccounts} />
          <Container
          style={{ display: 'flex', justifyContent: 'center', width: '60%' }}>
            <Button loading={isSubmitting || loadingAcc} content='Create' positive type='submit' circular/>
          </Container>
        </Form>
      )}
    </Formik>
  )
}

export default observer(FutureSavingAdd)