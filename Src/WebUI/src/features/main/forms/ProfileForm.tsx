import { Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import React, { useEffect } from 'react'
import { Button, Container, Dimmer, Divider, DropdownItemProps, Form, Grid, Header, Loader } from 'semantic-ui-react'
import MyTextInput from '../../../app/common/forms/MyTextInput'
import MyDropdown from '../../../app/common/forms/MyDropdown'
import MyDateInput from '../../../app/common/forms/MyDateInput'
import { useNavigate } from 'react-router-dom'
import { useStore } from '../../../app/stores/store'
import * as Yup from 'yup';
import { ProfileFormValues } from '../../../app/models/profile'
import { prettyFormat } from '@testing-library/react'
import LoadingComponent from '../../../app/common/components/LoadingComponent'
import { PasswordChangeFormValues } from '../../../app/models/user'
import ProfileDeleteForm from './ProfileDeleteForm'
import { toast } from 'react-toastify'

interface Props {
  username: string
}

function ProfileForm({username}: Props) {  
  const {modalStore, profileStore} = useStore()
  const {loading, loadProfile, updateProfile, profile, changePassword} = profileStore
  
  useEffect(() => {
    if(!profile) loadProfile(username)
  }, [loadProfile, profile])

  //TODO
  //* Naprawić pobieranie user i profile z api
  //* Dokończyć profile i change password

  const validationSchema = Yup.object({
    email: Yup.string().required('Email is required'),
    username: Yup.string().required('Username name is required'),
    displayName: Yup.string().required('Display name is required'),
    budgetName: Yup.string().required('Budget name is required')
  })

  const passwordValidationSchema = Yup.object({
    newPassword: Yup.string().matches(
      /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,32}$/,
      'Password must contain at least one digit, one lowercase letter, one uppercase letter, and be 8-32 characters long.'
    ).required('New password is required'),
    oldPassword: Yup.string().required('Old password is required'),
  })

  function handleProfileUpdate(newProfile: ProfileFormValues): void {
    updateProfile(profile.username, newProfile)
    modalStore.closeModal()
  }

  function handlePasswordChange(form: PasswordChangeFormValues): void {
    changePassword(profile.username, form)
    // Dodać wylogowanie
    modalStore.closeModal()
  }

  function handleLogout(): void {
    throw new Error('Function not implemented.')
  }

  function handleProfileDelete(): void {
    modalStore.closeModal()
    modalStore.openModal(<ProfileDeleteForm />)
  }

  return (
    profile ?
    (<Grid>
      <Grid.Row textAlign='center'>
        <Grid.Column width={16}>
          <Header as='h1' content='Profile Settings' textAlign='center' />
          <Divider />
        </Grid.Column>
      </Grid.Row>
      <Grid.Row>
        <Grid.Column width={8} textAlign='center'>
          <Formik
            validationSchema={validationSchema}
            initialValues={{email: profile.email, username: profile.username, 
            displayName: profile.displayName, budgetName: profile.budgetName, newPassword: '', oldPassword: ''}}
            onSubmit={(values, {setErrors}) => handleProfileUpdate(values)}>
              {({handleSubmit, isSubmitting, errors}) => (
                <Form 
                className='ui form' 
                onSubmit={handleSubmit}
                autoComplete='off'>
                  <Header as='h3' content='Update your data' />
                  <MyTextInput placeholder='Email' name='email' />
                  <MyTextInput placeholder='Username' name='username' />
                  <MyTextInput placeholder='Display name' name='displayName' />
                  <MyTextInput placeholder='Budget name' name='budgetName' />
                  <Button primary positive content='Accept' type='submit'/>
                </Form>
              )}
          </Formik>
        </Grid.Column>
        <Grid.Column width={1}>
          <Divider vertical/>
        </Grid.Column>
        <Grid.Column width={7} textAlign='center'>
          <Formik
          validationSchema={passwordValidationSchema}
          initialValues={{newPassword: '', oldPassword: ''}}
          onSubmit={(values, {setErrors}) => handlePasswordChange(values)}>
            {({handleSubmit, isSubmitting, errors}) => (
              <Form className='ui form' 
              onSubmit={handleSubmit}
              autoComplete='off'>
                <Header as='h3' content='Change your password' />
                <MyTextInput placeholder='Old Password' name='oldPassword' type='password'/>
                <MyTextInput placeholder='New Password' name='newPassword' type='password'/>
                <Button primary positive content='Accept' type='submit'/>
              </Form>
            )}
          </Formik>
        </Grid.Column>
      </Grid.Row>
      <Grid.Row textAlign='center'>
        <Grid.Column width={16}>
          <Button primary negative content='Log out' icon='sign-out' onClick={() => handleLogout()}/>
          <Button primary negative content='Delete profile' icon='trash' onClick={() => handleProfileDelete()}/>
        </Grid.Column>
      </Grid.Row>
    </Grid>
    ) 
    : (<LoadingComponent />)
  )
}

export default observer(ProfileForm)