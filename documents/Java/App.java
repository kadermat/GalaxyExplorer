package FileDelete.FileDelete;

import java.io.File;

/**
 * Hello world!
 *
 */
public class App {
	private static final String BASE_FILE_NAME = "K:\\airtraffic\\AirTrafficWorldwide24h_4096x2048_";
	private static final String FILE_ENDING = ".jpg";
	private static final String FOLDER = "K:\\airtraffic";

	public static void main(String[] args) {

		App app = new App();
		app.calcMaxAndMinSize();
	}

	
	public void calcMaxAndMinSize() {
		long minLength = Long.MAX_VALUE;
		long maxLength = 0;
		for (int i = 0; i < 2879; i += 1) {

		
				String number = createNumberForFileName(i);

				File file = new File(BASE_FILE_NAME + number + FILE_ENDING);
				if (file.exists()) {
					long filesize = file.length();
					if(filesize > maxLength) {
						maxLength = filesize;
					}
					if(filesize < minLength) {
						minLength = filesize;
					}
				}
			

		}
		System.out.println("min: " + minLength);
		System.out.println("max: " + maxLength);
	}
	
	public void del() {

		long length = 0;
		int fileCounter = 0;
		int counter = 0;
		for (int i = 0; i < 2879; i += 1) {

			if (counter < 2) {
				String number = createNumberForFileName(i);

				File file = new File(BASE_FILE_NAME + number + FILE_ENDING);
				if (file.exists()) {
					length += file.length();
					fileCounter++;
				}
			} else if(counter == 10){
				counter = 0;
			}
			counter++;

		}
		System.out.println("totalLength: " + length / Math.pow(2, 20));
		System.out.println("amount files: " + fileCounter);

	}

	private String createNumberForFileName(int number) {
		String numberAsString = String.valueOf(number);
		while (numberAsString.length() < 5) {

			numberAsString = '0' + numberAsString;
		}
		return numberAsString;

	}

}
