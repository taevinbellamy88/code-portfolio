import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { Field } from 'formik';
import siteReferenceService from '../../services/siteReferenceService';
import { Formik, Form } from 'formik';
import SiteReferenceValidation from '../../schema/user/siteReference';
import toastr from 'toastr';

import debug from 'sabio-debug';

const _logger = debug.extend('SiteRefModalContent');

const SiteRefModalContent = (props) => {
    const [formState, setFormState] = useState({
        arrayOfRadioBtns: [],
        referenceFields: [],
    });

    const mapRefField = (refType) => {
        const { id, name } = refType;
        return (
            <div className="form-check" key={`refType-${id}`}>
                <label htmlFor={id} className="form-check-label" style={{ marginLeft: '10px' }}>
                    <Field
                        className="form-check-input"
                        type="radio"
                        name="referenceId"
                        id={id}
                        value={id}
                        checked={onclick}
                    />
                    {name}
                </label>
            </div>
        );
    };

    useEffect(() => {
        siteReferenceService.getAllTypes().then(onGetSuccess).catch(onGetError);
    }, []);

    const onGetSuccess = (response) => {
        const typesArray = response.data.item;
        _logger('onGetTypesSuccess', typesArray);

        setFormState((prevState) => {
            const rd = { ...prevState };
            rd.arrayOfRadioBtns = typesArray;
            rd.referenceFields = typesArray.map(mapRefField);
            return rd;
        });
    };
    const onGetError = (error) => {
        toastr.error('Network Error - Please Check Connection and Try Again', error);
    };

    return (
        <>
            <div className="site-ref-form-container">
                <h1>Who Refered You:</h1>
                <Formik
                    enableReinitialize={true}
                    initialValues={props.currentSelection}
                    validationSchema={SiteReferenceValidation}>
                    <Form onChange={props.changeSelection}>
                        <div className="radio-button-container">{formState.referenceFields}</div>
                    </Form>
                </Formik>
            </div>
        </>
    );
};
SiteRefModalContent.propTypes = {
    changeSelection: PropTypes.func.isRequired,
    currentSelection: PropTypes.shape({ referenceId: PropTypes.number.isRequired }),
};

export default SiteRefModalContent;
