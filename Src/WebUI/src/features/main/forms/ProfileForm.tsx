import { Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import React, { useEffect } from 'react'
import { Button, Container, Divider, DropdownItemProps, Form, Grid, Header } from 'semantic-ui-react'
import MyTextInput from '../../../app/common/forms/MyTextInput'
import MyDropdown from '../../../app/common/forms/MyDropdown'
import MyDateInput from '../../../app/common/forms/MyDateInput'
import { useNavigate } from 'react-router-dom'
import { useStore } from '../../../app/stores/store'
import * as Yup from 'yup';
import { ProfileFormValues } from '../../../app/models/profile'
import { prettyFormat } from '@testing-library/react'

function ProfileForm() {  
  const {modalStore, profileStore, userStore} = useStore()
  const {loading, loadProfile, updateProfile, profile} = profileStore

  useEffect(() => {
    if(!userStore.user) userStore.getUser()
    if(!profile) loadProfile(userStore.user.username)
  }, [userStore.user.username, userStore.getUser, loadProfile])
  

  const validationSchema = Yup.object({
    email: Yup.string().required('Email is required'),
    username: Yup.string().required('Username name is required'),
    displayName: Yup.number().positive('Display name is required'),
    budgetName: Yup.string().required('Budget name is required')
  })

  function handleProfileUpdate(newProfile: ProfileFormValues): void {
    updateProfile(profile.username, newProfile)
  }

  function handlePasswordChange(): void {
    throw new Error('Function not implemented.')
  }

  function handleLogout(): void {
    throw new Error('Function not implemented.')
  }

  function handleProfileDelete(): void {
    throw new Error('Function not implemented.')
  }
/*
  <Grid.Column width={8} textAlign='center' >
  <Header as='h4' content='Change password' />
  <MyTextInput placeholder='New Password' name='newPassword' type='password'/>
  <MyTextInput placeholder='Old Password' name='oldPassword' type='password'/>
  <Button primary positive content='Accept' onClick={() => handlePasswordChange()}/>
  </Grid.Column>
  </Grid>
  <Divider />
  <Container style={{marginTop: '15px', display: 'flex', justifyContent: 'space-around'}}>
  <Button primary negative content='Logout' icon='logout' onClick={() => handleLogout()}/>
  <Button primary negative content='Delete Profile' icon='trash' onClick={() => handleProfileDelete()}/>
  </Container> */

  return (
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
          <Header as='h1' content='Profile Settings' textAlign='center' />
          <Divider />
          <Grid >
            <Grid.Column width={8} textAlign='center'>
              <Header as='h4' content='Update your data' />
              <MyTextInput placeholder='Email' name='email' />
              <MyTextInput placeholder='Username' name='username' />
              <MyTextInput placeholder='Display name' name='displayName' />
              <MyTextInput placeholder='Budget name' name='budgetName' />
              <Button primary positive content='Accept' type='submit'/>
            </Grid.Column>
            <Divider vertical/>
            </Grid>
        </Form>
      )}
    </Formik>
  )
}

export default observer(ProfileForm)