import React from 'react';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import DatePicker, { registerLocale } from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { authenticationService } from '../services';
import { userService } from '../services';
import PasswordField from 'material-ui-password-field';
import InputAdornment from '@material-ui/core/InputAdornment';
/* eslint-disable */
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import FaceIcon from '@material-ui/icons/Face';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import MailOutlineIcon from '@material-ui/icons/MailOutline';
import MeetingRoomIcon from '@material-ui/icons/MeetingRoom';
import Autocomplete from '@material-ui/lab/Autocomplete';

class RegisterPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            programms: [],
            levels: [],
            error: '',
        };

        if (authenticationService.currentUserValue) {
            this.props.history.push('/');
        }
    }

    componentDidMount() {

        userService.GetAllProgramms().then(programms => this.setState({ programms }));
        userService.GetAllLevels().then(levels => this.setState({ levels }));
    }

    render() {
        return (
            <Formik
                initialValues={{
                    fullName: '',
                    emailUser: '',
                    passwordUser: '',
                    confirmPassword: '',
                    dateOfBirth: new Date(),
                    progName: '',
                    level: '',
                    accessCode: ''
                }}

                validationSchema={Yup.object().shape({
                    fullName: Yup.string()
                        .required("Name is required."),
                    emailUser: Yup.string()
                        .email('Wrong format of email.')
                        .required("Email is required."),
                    passwordUser: Yup.string()
                        .min(8, 'Password must contain at least 8 symbols!').max(48, 'Password cannot be longer than 48 symbols!')
                        .required("Password is required."),
                    confirmPassword: Yup.string()
                        .oneOf([Yup.ref('passwordUser'), null], 'Passwords do not match!')
                        .required('You must confirm the password!'),
                    level: Yup.string()
                        .required("Level is required."),
                    progName: Yup.string()
                        .required("Programm name is required."),
                    accessCode: Yup.string()
                        .required("Enter access code."),
                    dateOfBirth: Yup.date()
                        .required("Date of birth is required.")

                })}
                onSubmit={({ fullName, emailUser, passwordUser, level, progName, accessCode, dateOfBirth }, { setStatus, setSubmitting }) => {
                    authenticationService.registerUser(fullName, emailUser, passwordUser, level, progName, accessCode, dateOfBirth)
                        .then(
                            user => {
                                this.props.history.push({
                                    pathname: '/',
                                });
                            },
                            error => {
                                setSubmitting(false);
                                setStatus(error);
                            }
                        );
                }}
            >
                {({ errors, status, touched, values, setFieldValue }) => (
                    <Form style={{ marginBottom: 7+"em" }}>

                        <Grid container
                            spacing={0}
                            direction="column"
                            alignItems="center"

                            style={{
                                minHeight: '90vh',
                            }}>
                            <Grid item xs={3} style={{ marginBottom: "2em" }}><h2>Registration</h2></Grid>
                            <hr />
                            <Grid container spacing={8} alignItems="flex-end" xs={3}>
                                <Grid item>
                                    <label htmlFor="fullName">Full name:</label>
                                </Grid>
                                <Grid item md={true} sm={true} xs={true}>
                                    <FormControl>
                                        <InputLabel htmlFor="input-with-icon-adornment">Full Name</InputLabel>
                                        <Input
                                            id="input-with-icon-adornment"
                                            onChange={fullName => setFieldValue('fullName', fullName.target.value)}
                                            name="fullName"
                                            startAdornment={
                                                <InputAdornment position="start">
                                                    <FaceIcon />
                                                </InputAdornment>
                                            }
                                        />

                                    </FormControl>

                                    <ErrorMessage name="fullName" component="div" className="invalid-feedback" />
                                </Grid>
                               
                            </Grid>
                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "-0.2em" }}>
                                <Grid item>
                                    <label htmlFor="emailUser">Email:</label>
                                </Grid>
                                <Grid item md={true} sm={true} xs={true}>
                                    <FormControl>
                                        <InputLabel htmlFor="input-with-icon-adornment">Email</InputLabel>
                                        <Input
                                            id="input-with-icon-adornment"
                                            onChange={emailUser => setFieldValue('emailUser', emailUser.target.value)}
                                            name="emailUser"
                                            startAdornment={
                                                <InputAdornment position="start">
                                                    <MailOutlineIcon />
                                                </InputAdornment>
                                            }
                                        />

                                    </FormControl>

                                    <ErrorMessage name="emailUser" component="div" className="invalid-feedback" />
                                </Grid>

                            </Grid>
                            <Grid container spacing={6} style={{
                                marginLeft: "-1.7em",
                                marginTop: ".3em",
                            }} alignItems="flex-start" xs={3}>
                                <Grid item>
                                    <label htmlFor="passwordUser" class="p-2">Password:</label>
                                </Grid>
                                <Grid item md={true} sm={true} xs={true}>

                                    <PasswordField
                                        name="passwordUser"
                                        onChange={passwordUser => setFieldValue('passwordUser', passwordUser.target.value)}
                                        style={{ marginLeft: "-1.2em" }}
                                    />
                                    <ErrorMessage name="passwordUser" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>
                            <Grid container spacing={6} style={{
                                marginLeft: "-1.7em",
                               
                            }} alignItems="flex-start" xs={3}>
                                <Grid item>
                                    <label htmlFor="confirmPassword" class="p-2">Confirm password:</label>
                                </Grid>
                                <Grid item md={true} sm={true} xs={true}>

                                    <PasswordField
                                        name="confirmPassword"
                                        onChange={confirmPassword => setFieldValue('confirmPassword', confirmPassword.target.value)}
                                        style={{ marginLeft: "-1.2em" }}
                                    />
                                    <ErrorMessage name="confirmPassword" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>
                            <Grid container spacing={8} alignItems="flex-end" xs={3}>
                                <Grid item>
                                    <label htmlFor="accessCode">Access code:</label>
                                </Grid>
                                <Grid item md={true} sm={true} xs={true}>
                                    <FormControl>
                                        <InputLabel htmlFor="input-with-icon-adornment">Access code</InputLabel>
                                        <Input
                                            id="input-with-icon-adornment"
                                            onChange={accessCode => setFieldValue('accessCode', accessCode.target.value)}
                                            name="accessCode"
                                            startAdornment={
                                                <InputAdornment position="start">
                                                    <MeetingRoomIcon />
                                                </InputAdornment>
                                            }
                                        />

                                    </FormControl>

                                    <ErrorMessage name="accessCode" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>
                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{marginTop: "1em"}}>
                            <Grid item md={true} sm={true} xs={true}>
                                    <Autocomplete
                                        id="combo-box-demo"
                                        options={this.state.levels}
                                        getOptionLabel={(option) => option.label}
                                        onChange
                                        style={{ width: 300 }}
                                        renderInput={(params) => <TextField {...params} label="Level" variant="outlined" />}
                                        onChange={(event, value) => setFieldValue('level', value.value)}
                                    />

                                    <ErrorMessage name="level" component="div" className="invalid-feedback" />
                            </Grid>
                            </Grid>
                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "1em" }}>
                                <Grid item md={true} sm={true} xs={true}>
                                    <Autocomplete
                                        id="combo-box-demo"
                                        options={this.state.programms}
                                        getOptionLabel={(option) => option.name}
                                        onChange
                                        style={{ width: 300 }}
                                        renderInput={(params) => <TextField {...params} label="Programm name" variant="outlined" />}
                                        onChange={(event, value) => setFieldValue('progName', value.value)}
                                    />

                                    <ErrorMessage name="progName" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>
                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "1em" }}>
                                <Grid item md={true} sm={true} xs={true}>
                                    <TextField
                                        id="date"
                                        label="Birthday"
                                        type="date"
                                        InputLabelProps={{
                                            shrink: true,
                                        }}
                                        onChange={dateOfBirth => setFieldValue('dateOfBirth', dateOfBirth.target.value)}
                                    />

                                    <ErrorMessage name="dateOfBirth" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>
                            <Grid container spacing={8} justify="flex-end" xs={3} style={{ marginTop: "2em", marginLeft: "-6.5em" }}>
                                <Button type="submit" variant="contained" color="secondary">
                                    Register
                                    </Button>
                             

                                {status &&
                                    <div className={'alert alert-danger'}>{status}</div>
                                }
                            </Grid>
                        </Grid>



                    </Form>
                )}
            </Formik>
        )
    }
}

export { RegisterPage };