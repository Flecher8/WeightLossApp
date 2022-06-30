import { useState } from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";

function ChangeAchievement(props) {
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
				props.setAchievement({
					Id: props.achievement.Id,
					Name: props.achievement.Name,
					Description: props.achievement.Description,
					RewardExperience: props.achievement.RewardExperience,
					ImgName: data.data.link
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
			fetch(constants.API_URL + "AchievementData", {
				method: props.method,
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				},

				body: JSON.stringify(props.achievement)
			})
				.then(res => res.json())
				.then(
					result => {
						// Update main table
						props.getAchievementData();

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
		<div className="ChangeAchievement">
			<Form noValidate validated={validated} onSubmit={handleSubmit}>
				<Modal.Header closeButton>
					<Modal.Title>{props.title}</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Label className="ms-1">Name</Form.Label>
							<Form.Control
								required
								pattern="^[A-ZА-ЯЁ][a-zа-яё]*$"
								type="text"
								maxLength="50"
								placeholder="Name"
								value={props.achievement.Name}
								onChange={e =>
									props.setAchievement({
										Id: props.achievement.Id,
										Name: e.target.value,
										Description: props.achievement.Description,
										RewardExperience: props.achievement.RewardExperience,
										ImgName: props.achievement.ImgName
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom02">
							<Form.Label className="ms-1">Description</Form.Label>
							<Form.Control
								required
								type="text"
								maxLength="150"
								placeholder="Description"
								value={props.achievement.Description}
								onChange={e =>
									props.setAchievement({
										Id: props.achievement.Id,
										Name: props.achievement.Name,
										Description: e.target.value,
										RewardExperience: props.achievement.RewardExperience,
										ImgName: props.achievement.ImgName
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom03">
							<Form.Label className="ms-1">RewardExperience</Form.Label>
							<Form.Control
								required
								pattern="^[1-9][0-9]+$"
								type="text"
								maxLength="150"
								placeholder="RewardExperience"
								value={props.achievement.RewardExperience}
								onChange={e =>
									props.setAchievement({
										Id: props.achievement.Id,
										Name: props.achievement.Name,
										Description: props.achievement.Description,
										RewardExperience: e.target.value,
										ImgName: props.achievement.ImgName
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						{/* if the object is being edited - display an image */}
						{props.method === "PUT" && (
							<div className="container border mb-3">
								<p>Image now</p>
								<img src={props.achievement.ImgName} alt="unloaded img" width="150px" />
							</div>
						)}
						<Form.Group as={Col} controlId="validationCustom04">
							<Form.Label className="ms-1">Image</Form.Label>
							<Form.Control type="file" placeholder="ImgName" onChange={e => saveImg(e)} />
						</Form.Group>
					</Row>
				</Modal.Body>
				<Modal.Footer>
					<Button variant="primary" type="submit">
						{props.title}
					</Button>
				</Modal.Footer>
			</Form>
		</div>
	);
}

export default ChangeAchievement;
