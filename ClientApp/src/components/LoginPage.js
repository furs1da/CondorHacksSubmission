import React from 'react';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { Link } from 'react-router-dom';
import { authenticationService } from '../services';
import PasswordField from 'material-ui-password-field';
import InputAdornment from '@material-ui/core/InputAdornment';
/* eslint-disable */
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import LockOpenIcon from '@material-ui/icons/LockOpen';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';

class LoginPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            showPassword: false,
        };

        if (authenticationService.currentUserValue) {
            this.props.history.push('/');
        }
    }
    onTogglePassword = () =>
        this.setState(prevState => ({
            showPassword: !prevState.showPassword,
        }));

    render() {
        return (
            <div >
                <Formik
                    initialValues={{
                        emailUser: '',
                        passwordUser: ''
                    }}
                    validationSchema={Yup.object().shape({
                        emailUser: Yup.string().email('Wrong format of email.').required('Email is required'),
                        passwordUser: Yup.string().required('Password is required')
                    })}
                    onSubmit={({ emailUser, passwordUser }, { setStatus, setSubmitting }) => {
                        authenticationService.login(emailUser, passwordUser)
                            .then(
                                user => {
                                    const { from } = this.props.location.state || { from: { pathname: "/" } };
                                    this.props.history.push(from);
                                },
                                error => {
                                    setSubmitting(false);
                                    setStatus(error);
                                }
                            );
                    }}
                >
                    {({ errors, touched, status, isSubmitting, setFieldValue }) => (
                        <Form>
                            <Grid container
                                spacing={0}
                                direction="column"
                                alignItems="center"

                                style={{
                                    minHeight: '90vh',
                                }}>
                                <Grid item xs={3} style={{ marginBottom: "2em" }}><h2>Login</h2></Grid>
                                <Grid container spacing={8} alignItems="flex-end" xs={3}>
                                    <Grid item>
                                        <label htmlFor="email">Email:</label>
                                    </Grid>
                                    <Grid item md={true} sm={true} xs={true}>
                                        <FormControl>
                                            <InputLabel htmlFor="input-with-icon-adornment">Email</InputLabel>
                                            <Input
                                                id="input-with-icon-adornment"

                                                onChange={emailUser => setFieldValue('emailUser', emailUser.target.value)}
                                                name="email"
                                                startAdornment={
                                                    <InputAdornment position="start">
                                                        <LockOpenIcon />
                                                    </InputAdornment>
                                                }
                                            />

                                        </FormControl>

                                        <ErrorMessage name="email" component="div" className="invalid-feedback" />
                                    </Grid>
                                </Grid>
                                <Grid container spacing={6} style={{
                                    marginLeft: "-1.7em",
                                    marginTop: "2em"
                                }} alignItems="flex-start" xs={3}>
                                    <Grid item>
                                        <label htmlFor="password" class="p-2">Password:</label>
                                    </Grid>
                                    <Grid item md={true} sm={true} xs={true}>

                                        <PasswordField
                                            name="password"
                                            onChange={passwordUser => setFieldValue('passwordUser', passwordUser.target.value)}
                                            style={{ marginLeft: "-1.2em" }}
                                        />
                                        <ErrorMessage name="password" component="div" className="invalid-feedback" />
                                    </Grid>
                                </Grid>
                                <Grid container spacing={8} justify="flex-end" xs={3} style={{ marginTop: "2em", marginLeft: "-6.5em" }}>
                                    <Button type="submit" variant="contained" color="secondary">
                                        Login
                                    </Button>
                                    <Link to="/register" className="pull-right" style={{ fontSize: '.9em', marginLeft: '0.5em', marginTop: ".5em", color: "	#FF1493" }}>Register</Link>

                                    {status &&
                                        <div className={'alert alert-danger'}>{status}</div>
                                    }
                                </Grid>
                            </Grid>
                        </Form>
                    )}
                </Formik>
            </div>
        )
    }
}

export { LoginPage };