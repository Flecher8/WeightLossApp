import { useState } from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";

function ChangeDesignTheme(props) {
    const [validated, setValidated] = useState(false);
    
    // Save img in imgur and write img to object
	function saveImg(ev) {
		const formdata = new FormData();
		formdata.append("image", ev.target.files[0]);
		fetch(constants.API_Imgur, {
			method: "post",
			headers: {
				Authorization: "Client-ID " + constants.Client_ID,
				Accept: "application/json"
			},
			body: formdata
		})
			.then(res => res.json())
			.then(data => {
				props.setDesignTheme({
					Id: props.designTheme.Id,
					BaseColor: props.designTheme.BaseColor,
					SecondaryColor: props.designTheme.SecondaryColor,
					AccentColor: props.designTheme.AccentColor,
					IconImage: data.data.link,
				});
			});
	}

    // Fetch function
	const handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			event.preventDefault();
			fetch(constants.API_URL + "DesignThemeData", {
				method: props.method,
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				},

				body: JSON.stringify(props.designTheme)
			})
				.then(res => res.json())
				.then(
					result => {
						// Update main table
						props.getDesignThemes();

						// Close
						props.state();
					},
					error => {
						alert("Failed");
					}
				);
		}

		setValidated(true);
	};

    return (
        <div className="ChangeDesignTheme">
            <Form noValidate validated={validated} onSubmit={handleSubmit}>
                <Modal.Header closeButton>
					<Modal.Title>{props.title}</Modal.Title>
				</Modal.Header>
                <Modal.Body>
                    <Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Label className="ms-1">Base Color</Form.Label>
							<Form.Control
								required
								pattern="^#(?:[0-9a-fA-F]{3}){1,2}$"
								type="text"
								maxLength="7"
								placeholder="Section"
								value={props.designTheme.BaseColor}
								onChange={e =>
									props.setDesignTheme({
										Id: props.designTheme.Id,
										BaseColor: e.target.value,
										SecondaryColor: props.designTheme.SecondaryColor,
										AccentColor: props.designTheme.AccentColor,
										IconImage: props.designTheme.IconImage
									})
								}
							/>
						</Form.Group>
					</Row>
                    <Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom02">
							<Form.Label className="ms-1">Secondary Color</Form.Label>
							<Form.Control
								required
								pattern="^#(?:[0-9a-fA-F]{3}){1,2}$"
								type="text"
								maxLength="7"
								placeholder="Section"
								value={props.designTheme.SecondaryColor}
								onChange={e =>
									props.setDesignTheme({
										Id: props.designTheme.Id,
										BaseColor: props.designTheme.BaseColor,
										SecondaryColor: e.target.value,
										AccentColor: props.designTheme.AccentColor,
										IconImage: props.designTheme.IconImage
									})
								}
							/>
						</Form.Group>
					</Row>
                </Modal.Body>
            </Form>
        </div>
    )
}